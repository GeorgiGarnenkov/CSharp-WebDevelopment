using System;
using System.Linq;
using System.Threading.Tasks;
using Chushka.Data;
using Chushka.Data.Models;
using Chushka.Data.Models.Enums;
using Chushka.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chushka.Controllers
{
    public class ProductsController : BaseController
    {
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.Db
                                    .Products
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(product);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                var product = new Product()
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    Price = viewModel.Price,
                    Type = Enum.Parse<ProductType>(viewModel.Type)
                };

                await this.Db
                    .Products
                    .AddAsync(product);
                await this.Db
                    .SaveChangesAsync();

                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.Db
                             .Products
                             .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditDeleteViewModel viewModel)
        {
            var product = this.Db
                .Products
                .FirstOrDefault(x => x.Id == viewModel.Id);

            if (product == null || !this.ModelState.IsValid)
            {
                return this.RedirectToAction("Index", "Home");
            }

            product.Type = (ProductType)Enum.Parse(typeof(ProductType), viewModel.Type);
            product.Description = viewModel.Description;
            product.Name = viewModel.Name;
            product.Price = viewModel.Price;

            this.Db.Products.Update(product);
            this.Db.SaveChanges();

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = await this.Db
                                    .Products
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EditDeleteViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var product = await this.Db
                .Products
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (product == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            this.Db
                .Products
                .Remove(product);

            await this.Db
                .SaveChangesAsync();

            return this.RedirectToAction("Index", "Home");
        }
    }
}
