using System.Collections.Generic;

namespace PANDAWebApp.ViewModels.Packages.AdminPackages
{
    public class PendingPackagesViewModel
    {
        public IEnumerable<PackageAdminBaseViewModel> PendingPackages { get; set; }
    }
}