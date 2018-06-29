namespace MuTube.Web.Controllers
{
    using MuTube.Data;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;

    public class BaseController : Controller
    {
        protected const string ErrorKey = "error";
        protected const string TopMenuKey = "topMenu";
        protected BaseController()
        {
            this.Context = new MeTubeContext();
            this.Model.Data[ErrorKey] = string.Empty;
        }

        protected MeTubeContext Context { get; private set; }

        protected IActionResult RedirectToHome()
        {
            return this.RedirectToAction("/home/index");
        }

        public override void OnAuthentication()
        {
            this.Model.Data[TopMenuKey] = string.Empty;
            if (this.User.IsAuthenticated)
            {
                this.Model.Data[TopMenuKey] = @"<li class=""nav-item active col-md-3"">
	                                                <a class=""nav-link h5"" href=""/home/index"">Home</a>
                                                </li>
                                                <li class=""nav-item active col-md-3"">
	                                                <a class=""nav-link h5"" href=""/users/profile"">Profile</a>
                                                </li>
                                                <li class=""nav-item active col-md-3"">
	                                                <a class=""nav-link h5"" href=""/tubes/upload"">Upload</a>
                                                </li>
                                                <li class=""nav-item active col-md-3"">
	                                                <a class=""nav-link h5"" href=""/users/logout"">Logout</a>
                                                </li>";
            }
            else
            {
                this.Model.Data[TopMenuKey] = @"<li class=""nav-item active col-md-4"">
	                                                <a class=""nav-link h5"" href=""/home/index"">Home</a>
                                                </li>
                                                <li class=""nav-item active col-md-4"">
	                                                <a class=""nav-link h5"" href=""/users/login"">Login</a>
                                                </li>
                                                <li class=""nav-item active col-md-4"">
	                                                <a class=""nav-link h5"" href=""/users/register"">Register</a>
                                                </li>";
            }

            base.OnAuthentication();
        }

        protected virtual IActionResult BuildErrorView()
        {
            this.Model.Data[ErrorKey] = "You have errors in the form!";
            return this.View();
        }
    }
}