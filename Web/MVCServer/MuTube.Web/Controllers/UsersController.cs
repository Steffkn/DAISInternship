using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuTube.Web.Controllers
{
    public class UsersController : BaseController
    {
        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(int i)
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(int i)
        {
            return this.View();
        }
    }
}
