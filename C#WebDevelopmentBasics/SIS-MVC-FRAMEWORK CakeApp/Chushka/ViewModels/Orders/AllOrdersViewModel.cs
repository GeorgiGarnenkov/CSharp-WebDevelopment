using System.Collections.Generic;

namespace Chushka.ViewModels.Orders
{
    public class AllOrdersViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }
    }
}