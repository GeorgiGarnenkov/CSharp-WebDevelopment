using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace IRunes.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index(IHttpRequest request)
        {
            var username = this.GetUsername(request);
            if (username == null)
            {
                return this.View();
            }

            this.ViewBag["username"] = username;

            return this.View();
        }
    }
}