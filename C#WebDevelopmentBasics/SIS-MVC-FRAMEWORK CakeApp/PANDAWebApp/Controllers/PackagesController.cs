using System;
using System.Linq;
using PANDAWebApp.Models;
using PANDAWebApp.ViewModels.Packages;
using PANDAWebApp.ViewModels.Packages.AdminPackages;
using SIS.HTTP.Responses;
using SIS.MvcFramework;

namespace PANDAWebApp.Controllers
{
    public class PackagesController : BaseController
    {
        [Authorize]
        public IHttpResponse Details(int id)
        {
            var viewModel = this.Db
                .Packages
                .Where(x => x.Id == id)
                .Select(x => new PackageDetailsViewModel
                {
                    Description = x.Description,
                    ShippingAddress = x.ShippingAddress,
                    Recipient = x.Recipient.Username,
                    Weight = x.Weight,
                    Status = x.Status.ToString(),
                    EstimatedDeliveryDate = x.EstimatedDeliveryDate == null ? x.EstimatedDeliveryDate.ToString() : x.EstimatedDeliveryDate.Value.ToString("d"),

                })
                .FirstOrDefault();

            if (viewModel == null)
            {
                return this.BadRequestError("Invalid package id.");
            }

            return this.View(viewModel);
        }

        [Authorize("Admin")]
        public IHttpResponse Create()
        {
            CollectionRecipientViewModel view = new CollectionRecipientViewModel
            {
                Usernames = this.Db
                   .Users
                   .Select(x => new RecipientViewModel
                   {
                       Username = x.Username
                   })
            };

            return this.View("/Packages/Admin/Create", view);
        }

        [Authorize("Admin")]
        [HttpPost]
        public IHttpResponse Create(CreatePackageInputViewModel model)
        {
            var recipient = this.Db.Users.FirstOrDefault(x => x.Username == model.Username);

            var package = new Package
            {
                Description = model.Description,
                RecipientId = recipient.Id,
                Weight = model.Weight,
                EstimatedDeliveryDate = null,
                ShippingAddress = model.ShippingAddress,
                Status = PackageStatus.Pending
            };

            this.Db.Packages.Add(package);
            this.Db.SaveChanges();

            return this.Redirect("/");
        }

        [Authorize("Admin")]
        public IHttpResponse Pending()
        {
            var pendingPackages = new PendingPackagesViewModel()
            {
                PendingPackages = this.Db.Packages.Where(x => x.Status == PackageStatus.Pending)
                    .Select(x => new PackageAdminBaseViewModel()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Weight = x.Weight,
                        ShippingAddress = x.ShippingAddress,
                        Recipient = x.Recipient.Username,
                    })
            };

            return this.View("/Packages/Admin/Pending", pendingPackages);
        }

        [Authorize("Admin")]
        public IHttpResponse Shipped()
        {
            var shippedPackages = new ShippedPackagesViewModel()
            {
                ShippedPackages = this.Db.Packages.Where(x => x.Status == PackageStatus.Shipped)
                    .Select(x => new PackageAdminBaseViewModel()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Weight = x.Weight,
                        EstimatedDeliveryDate = x.EstimatedDeliveryDate == null ? x.EstimatedDeliveryDate.ToString() : x.EstimatedDeliveryDate.Value.ToString("d"),
                        Recipient = x.Recipient.Username,
                    })
            };

            return this.View("/Packages/Admin/Shipped", shippedPackages);
        }

        [Authorize("Admin")]
        public IHttpResponse Delivered()
        {
            var deliveredPackage = new DeliveredPackageViewModel()
            {
                DeliveredPackages = this.Db.Packages.Where(x => x.Status == PackageStatus.Delivered)
                    .Select(x => new PackageAdminBaseViewModel()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Weight = x.Weight,
                        ShippingAddress = x.ShippingAddress,
                        Recipient = x.Recipient.Username,
                    })
            };

            return this.View("/Packages/Admin/Delivered", deliveredPackage);
        }

        [Authorize("Admin")]
        public IHttpResponse Ship(int id)
        {
            var package = this.Db.Packages.FirstOrDefault(x => x.Id == id);

            if (package == null)
            {
                return this.BadRequestError("Package not found!");
            }

            var randomDays = new Random().Next(20, 41);

            package.EstimatedDeliveryDate = DateTime.Now.AddDays(randomDays);

            package.Status = PackageStatus.Shipped;

            this.Db.SaveChanges();

            return this.Redirect("/");
        }

        [Authorize("Admin")]
        public IHttpResponse Deliver(int id)
        {
            var package = this.Db.Packages.FirstOrDefault(x => x.Id == id);

            if (package == null)
            {
                return this.BadRequestError("Package not found!");
            }

            package.Status = PackageStatus.Delivered;

            this.Db.SaveChanges();

            return this.Redirect("/");
        }

        [Authorize]
        public IHttpResponse Acquire(int id)
        {
            var package = this.Db.Packages.FirstOrDefault(x => x.Id == id);
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);

            if (package == null)
            {
                return this.BadRequestError("Package not found!");
            }

            var receipt = new Receipt()
            {
                RecipientId = user.Id,
                PackageId = package.Id,
                IssuedOn = DateTime.Now,
                Fee = (decimal)package.Weight * 2.67m
            };

            package.Status = PackageStatus.Acquired;

            this.Db.Receipts.Add(receipt);
            this.Db.SaveChanges();

            return this.Redirect("/");
        }

    }
}