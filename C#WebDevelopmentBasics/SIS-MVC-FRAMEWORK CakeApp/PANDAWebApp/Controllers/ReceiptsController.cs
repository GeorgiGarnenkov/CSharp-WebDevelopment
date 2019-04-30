using System.Linq;
using PANDAWebApp.ViewModels.Receipts;
using SIS.HTTP.Responses;
using SIS.MvcFramework;

namespace PANDAWebApp.Controllers
{
    public class ReceiptsController : BaseController
    {
        [Authorize]
        public IHttpResponse Index()
        {
            var allReceipts = new AllReceiptsIndexViewModel
            {
                AllReceipts = this.Db
                    .Receipts.Where(x => x.Recipient.Username == this.User.Username)
                             .Select(x => new ReceiptBaseViewModel()
                             {
                                 Id = x.Id,
                                 Fee = x.Fee,
                                 IssuedOn = x.IssuedOn.ToString("d"),
                                 Recipient = x.Recipient.Username
                             }),
                
            };

            return this.View(allReceipts);
        }

        [Authorize]
        public IHttpResponse Details(int id)
        {
            var viewModel = this.Db
                .Receipts
                .Where(x => x.Id == id && this.User.Username == x.Recipient.Username)
                .Select(x => new ReceiptDetailsViewModel()
                {
                    Id = x.Id,
                    Recipient = x.Recipient.Username,
                    Description = x.Package.Description,
                    Weight = x.Package.Weight,
                    DeliveryAddress = x.Package.ShippingAddress,
                    IssuedOn = x.IssuedOn.ToString("d"),
                    Total = x.Package.Weight * 2.67

                })
                .FirstOrDefault();

            if (viewModel == null)
            {
                return this.Redirect("/");
            }

            return this.View("/Receipts/Details", viewModel);
        }
    }
}