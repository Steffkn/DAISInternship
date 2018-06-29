namespace MuTube.Web.Controllers
{
    using MeTube.Models;
    using MuTube.Web.Attributes;
    using MuTube.Web.Models;
    using MuTube.Web.Models.ViewModels;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System.Linq;

    public class TubesController : BaseController
    {
        [HttpGet]
        [AithorizeLogin]
        public IActionResult Upload()
        {
            return this.View();
        }

        [HttpPost]
        [AithorizeLogin]
        public IActionResult Upload(UploadTubeBindingModel model)
        {
            if (!this.IsValidModel(model))
            {
                return this.BuildErrorView();
            }
            using (this.Context)
            {
                var user = this.Context.Users.FirstOrDefault(u => u.Username == this.User.Name);
                var youtubeId = GetTubeId(model.YouTubeLink);

                if (string.IsNullOrWhiteSpace(youtubeId))
                {
                    return this.BuildErrorView();
                }

                var tube = new Tube()
                {
                    Author = model.Author,
                    Title = model.Title,
                    Description = model.Description,
                    YoutubeId = youtubeId,
                    UserId = user.Id,
                };

                this.Context.Tubes.Add(tube);
                this.Context.SaveChanges();
                return this.RedirectToAction($"/tubes/details?id={tube.Id}");
            }
        }

        [HttpGet]
        [AithorizeLogin]
        public IActionResult Details(int id)
        {
            using (this.Context)
            {
                var tube = this.Context.Tubes.Find(id);
                if (tube == null)
                {
                    return this.RedirectToHome();
                }

                tube.Views++;
                this.Context.SaveChanges();

                this.Model.Data["tittle"] = tube.Title;
                this.Model.Data["author"] = tube.Author;
                this.Model.Data["views"] = string.Format("{0} View{1}", tube.Views.ToString(), tube.Views == 1 ? "" : "s");
                this.Model.Data["description"] = tube.Description;
                this.Model.Data["youtubeId"] = tube.YoutubeId;

                return this.View();
            }
        }

        private string GetTubeId(string youtubeLink)
        {
            var resultId = string.Empty;
            if (youtubeLink.ToLower().Contains("youtube.com"))
            {
                resultId = youtubeLink.Split("v=")[1].Substring(0, 11);
            }
            else if (youtubeLink.ToLower().Contains("youtu.be"))
            {
                resultId = youtubeLink.Split("/").Last().Substring(0, 11);
            }

            return resultId;
        }
    }
}
