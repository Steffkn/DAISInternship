namespace MuTube.Web
{
    using MuTube.Data;
    using SimpleMvc.Framework;
    using SimpleMvc.Framework.Routers;
    using WebServer;

    public class Launcher
    {
        static void Main()
        {
            var context = new MeTubeContext();
            var server = new WebServer(8000, new ControllerRouter(), new ResourceRouter());
            MvcEngine.Run(server, context);
        }
    }
}
