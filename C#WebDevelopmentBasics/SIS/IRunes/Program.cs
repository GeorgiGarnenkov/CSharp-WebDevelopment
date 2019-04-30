using IRunes.Controllers;
using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Routing;

namespace IRunes
{
    class Program
    {
        static void Main()
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            
            //var handler = new ControllerRouter();
            //Server server = new Server(80, handler);
            //var engine = new MvcEngine();
            //engine.Run(server);

            serverRoutingTable.Routes[HttpRequestMethod.GET]["/"] = request =>
                new HomeController().Index(request);
            serverRoutingTable.Routes[HttpRequestMethod.GET]["/home/index"] = request =>
                new HomeController().Index(request);
            
            serverRoutingTable.Routes[HttpRequestMethod.GET]["/users/login"] = request =>
                new UsersController().Login(request);
            serverRoutingTable.Routes[HttpRequestMethod.POST]["/users/login"] = request => 
                new UsersController().LoginPost(request);
            serverRoutingTable.Routes[HttpRequestMethod.GET]["/users/register"] = request => 
                new UsersController().Register(request);
            serverRoutingTable.Routes[HttpRequestMethod.POST]["/users/register"] = request => 
                new UsersController().RegisterPost(request);
            serverRoutingTable.Routes[HttpRequestMethod.GET]["/users/logout"] = request => 
                new UsersController().Logout(request);
            
            serverRoutingTable.Routes[HttpRequestMethod.GET]["/albums/all"] = request =>
                new AlbumsController().All(request);
            serverRoutingTable.Routes[HttpRequestMethod.GET]["/albums/create"] = request =>
                new AlbumsController().Create(request);
            serverRoutingTable.Routes[HttpRequestMethod.POST]["/albums/create"] = request =>
                new AlbumsController().CreatePost(request);
            serverRoutingTable.Routes[HttpRequestMethod.GET]["/albums/details"] = request =>
                new AlbumsController().Details(request);
            
            serverRoutingTable.Routes[HttpRequestMethod.GET]["/tracks/create"] = request =>
                new TracksController().Create(request);
            serverRoutingTable.Routes[HttpRequestMethod.POST]["/tracks/create"] = request =>
                new TracksController().CreatePost(request);
            serverRoutingTable.Routes[HttpRequestMethod.GET]["/tracks/details"] = request =>
                new TracksController().Details(request);



            Server server = new Server(80, serverRoutingTable);

            server.Run();
        }
    }
}