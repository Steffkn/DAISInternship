namespace MuTube.Web.Controllers
{
    using SimpleMvc.Framework.Interfaces;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
