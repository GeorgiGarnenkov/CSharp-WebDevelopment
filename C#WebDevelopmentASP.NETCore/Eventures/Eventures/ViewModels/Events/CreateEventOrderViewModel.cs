using System.Collections.Generic;
using Eventures.ViewModels.Orders;

namespace Eventures.ViewModels.Events
{
    public class CreateEventOrderViewModel
    {
        public IEnumerable<CreateEventViewModel> CreateEventViewModels { get; set; }

        public CreateOrderViewModel CreateOrderViewModel { get; set; }
    }
}
