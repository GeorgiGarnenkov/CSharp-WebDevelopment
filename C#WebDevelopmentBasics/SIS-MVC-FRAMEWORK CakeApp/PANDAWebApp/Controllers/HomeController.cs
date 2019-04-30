using System.Linq;
using PANDAWebApp.Models;
using PANDAWebApp.ViewModels.Home;
using SIS.HTTP.Responses;

namespace PANDAWebApp.Controllers
{
    public class HomeController : BaseController
    {

        public IHttpResponse Index()
        {
            if (this.User.IsLoggedIn)
            {
                var allPackages = new AllPackagesIndexViewModel
                {
                    PendingPackages = this.Db
                        .Packages.Where(x => x.Recipient.Username == this.User.Username &&
                                             x.Status == PackageStatus.Pending)
                        .Select(x => new PackageBaseViewModel
                        {
                            Id = x.Id,
                            Description = x.Description
                        }),
                    ShippedPackages = this.Db
                        .Packages.Where(x => x.Recipient.Username == this.User.Username &&
                                             x.Status == PackageStatus.Shipped)
                        .Select(x => new PackageBaseViewModel
                        {
                            Id = x.Id,
                            Description = x.Description
                        }),
                    DeliveredPackages = this.Db
                        .Packages.Where(x => x.Recipient.Username == this.User.Username &&
                                             x.Status == PackageStatus.Delivered)
                        .Select(x => new PackageBaseViewModel
                        {
                            Id = x.Id,
                            Description = x.Description
                        }),
                };

                return this.View("/Home/IndexLoggedIn", allPackages);
            }
            
            return this.View();
        }
    }
}