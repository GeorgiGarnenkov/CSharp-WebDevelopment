using System.Diagnostics;
using System.Linq;
using Chushka.Data;
using Microsoft.AspNetCore.Mvc;
using Chushka.ViewModels;

namespace Chushka.Controllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            this.Db = new ChushkaDbContext();
        }

        protected ChushkaDbContext Db { get; }
       
    }
}
