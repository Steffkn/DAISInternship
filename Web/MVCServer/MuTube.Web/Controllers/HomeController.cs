namespace MuTube.Web.Controllers
{
    using MuTube.Web.Models.ViewModels;
    using SimpleMvc.Framework.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            this.Model.Data["result"] = string.Empty;

            if (!this.User.IsAuthenticated)
            {
                this.Model.Data["userGreeting"] =
                    @"<div class=""jumbotron"">
                        <p class=""h1 display-3"">Welcome to MeTube&trade;!</p>
                        <p class=""h3"">The simplest, easiest to use, most comfortable Multimedia Application.</p>
                        <hr class=""my-3"">
                        <p><a href=""/users/login"">Login</a> if you have an account or <a href=""/users/register"">Register</a> now and start tubing.</p>
                    </div>";
            }
            else
            {
                this.Model.Data["userGreeting"] =
                    $@"<div class=""text-center"">
                            <h3 class=""text-info"">Welcome, {this.User.Name}</h3>
                        </div>
                        <hr class=""my-4"">";

                var tubes = new List<TubeProfileViewModel>();
                using (this.Context)
                {
                    tubes = this.Context.Tubes
                        .Select(TubeProfileViewModel.FromTube)
                        .ToList();
                }

                var tubesResult = new StringBuilder();

                tubesResult.AppendLine(@"<div class=""row text-center"">");
                for (int i = 1; i <= tubes.Count; i++)
                {
                    if (i % 3 == 1 && i != 1)
                    {
                        tubesResult.AppendLine("</div>");
                        tubesResult.AppendLine(@"<div class=""row text-center"">");
                    }

                    var tube = tubes[i - 1];
                    tubesResult.AppendFormat(
                        $@"<div class=""col-4"">
                            <div>
                                <a href=""/tubes/details?id={tube.Id}""><img class=""img-thumbnail"" src=""https://img.youtube.com/vi/{tube.YoutubeId}/hqdefault.jpg"" alt=""{tube.Title}"" /></a>
                                <h4>{tube.Title}</h5>
                                <h5><em>{tube.Author}</em></h4>
                            </div>
                        </div>");
                }
                tubesResult.AppendLine("</div>");

                this.Model.Data["result"] = tubesResult.ToString();
            }
            return this.View();
        }
    }
}
