using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeTube.Models;
using MuTube.Web.Attributes;
using MuTube.Web.Models;
using MuTube.Web.Models.ViewModels;
using SimpleMvc.Common;
using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Interfaces;

namespace MuTube.Web.Controllers
{
    public class UsersController : BaseController
    {
        [HttpGet]
        public IActionResult Login()
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginBindingModel model)
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!this.IsValidModel(model))
            {
                return this.BuildErrorView();
            }

            User user;
            using (this.Context)
            {
                user = this.Context.Users.FirstOrDefault(u => u.Username == model.Username);
            }

            string passwordHash = PasswordUtilities.GetPasswordHash(model.Password);
            if (user == null || user.PasswordHash != passwordHash)
            {
                return this.BuildErrorView();
            }

            this.SignIn(user.Username, user.Id);

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisteringBindingModel model)
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!this.IsValidModel(model))
            {
                return this.BuildErrorView();
            }

            string passwordHash = PasswordUtilities.GetPasswordHash(model.Password);
            var user = new User()
            {
                PasswordHash = passwordHash,
                Email = model.Email,
                Username = model.Username
            };

            using (this.Context)
            {
                this.Context.Users.Add(user);
                this.Context.SaveChanges();
            }

            this.SignIn(user.Username, user.Id);

            return this.RedirectToHome();
        }


        [HttpGet]
        public IActionResult Logout()
        {
            if (this.User.IsAuthenticated)
            {
                this.SignOut();
            }

            return this.RedirectToHome();
        }

        [HttpGet]
        [AithorizeLogin]
        public IActionResult Profile()
        {
            List<TubeProfileViewModel> tubes = new List<TubeProfileViewModel>();
            using (this.Context)
            {
                var user = this.Context.Users.FirstOrDefault(u => u.Username == this.User.Name);
                tubes = this.Context.Tubes
                    .Where(t => t.UserId == user.Id)
                    .Select(TubeProfileViewModel.FromTube)
                    .ToList();
                this.Model.Data["username"] = user.Username;
                this.Model.Data["email"] = user.Email;
            }


            var result = new StringBuilder();

            for (int i = 0; i < tubes.Count; i++)
            {
                result.AppendFormat(
                    $@"<tr>
                        <td class=""font-weight-bold"">{i + 1}</td>
                        <td>{tubes[i].Title}</td>
                        <td>{tubes[i].Author}</td>
                        <td>
                            <a href=""/tubes/details?id={tubes[i].Id}"">Details</a>
                        </td>
                    </tr>");
            }

            this.Model.Data["tubes"] = result.ToString();
            return this.View();
        }
    }
}
