using PANDAWebApp.Data;
using SIS.MvcFramework;

namespace PANDAWebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            this.Db = new ApplicationDbContext();
        }

        public ApplicationDbContext Db { get; }
    }
}