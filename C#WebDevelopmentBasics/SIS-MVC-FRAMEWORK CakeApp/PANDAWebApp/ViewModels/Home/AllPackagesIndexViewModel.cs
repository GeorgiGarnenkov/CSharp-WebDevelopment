
using System.Collections.Generic;

namespace PANDAWebApp.ViewModels.Home
{
    public class AllPackagesIndexViewModel
    {
        public IEnumerable<PackageBaseViewModel> PendingPackages { get; set; }

        public IEnumerable<PackageBaseViewModel> ShippedPackages { get; set; }

        public IEnumerable<PackageBaseViewModel> DeliveredPackages { get; set; }
    }
}