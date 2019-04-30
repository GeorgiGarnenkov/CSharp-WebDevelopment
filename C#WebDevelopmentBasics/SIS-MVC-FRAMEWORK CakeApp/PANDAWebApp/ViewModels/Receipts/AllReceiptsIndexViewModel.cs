using System.Collections.Generic;

namespace PANDAWebApp.ViewModels.Receipts
{
    public class AllReceiptsIndexViewModel
    {
        public IEnumerable<ReceiptBaseViewModel> AllReceipts { get; set; }
    }
}