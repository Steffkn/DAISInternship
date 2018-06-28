using System;
using System.Linq;
using MeTube.Models;
using MuTube.Web.Models;
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
                this.Model.Data[ErrorKey] = "You have errors in the form!";
                return this.View();
            }

            User user;
            using (this.Context)
            {
                user = this.Context.Users.FirstOrDefault(u => u.Username == model.Username);
            }

            string passwordHash = PasswordUtilities.GetPasswordHash(model.Password);
            if (user == null || user.PasswordHash != passwordHash)
            {
                this.Model.Data[ErrorKey] = "You have errors in the form!";
                return this.View();
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
                this.Model.Data[ErrorKey] = "You have errors in the form!";
                return this.View();
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
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToAction("/users/login");
            }

            this.SignOut();

            return this.RedirectToHome();
        }
    }
}
