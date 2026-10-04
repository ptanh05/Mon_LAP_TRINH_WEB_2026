using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreMVCLAB5.Models;

namespace NetCoreMVCLAB5.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Danh sách danh mục mẫu
        public static readonly List<Category> Categories = new List<Category>
        {
            new Category { Id = 1, Name = "Điện thoại & Tablet" },
            new Category { Id = 2, Name = "Laptop & Máy tính" },
            new Category { Id = 3, Name = "Phụ kiện công nghệ" },
            new Category { Id = 4, Name = "Thiết bị âm thanh" }
        };

        // Danh sách sản phẩm mẫu ban đầu
        private static readonly List<Product> Products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "iPhone 15 Pro Max 256GB",
                Price = 30000000,
                SalePrice = 26500000,
                Image = "/products/sample_iphone.jpg",
                Description = "Điện thoại iPhone 15 Pro Max chính hãng VN/A, khung viền titan siêu bền.",
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Laptop ASUS Zenbook 14 OLED",
                Price = 24000000,
                SalePrice = 21000000,
                Image = "/products/sample_laptop.jpg",
                Description = "Laptop siêu mỏng nhẹ với màn hình OLED sắc nét, Intel Core Ultra 7.",
                CategoryId = 2
            }
        };

        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: /Product
        public IActionResult Index()
        {
            foreach (var p in Products)
            {
                p.Category = Categories.FirstOrDefault(c => c.Id == p.CategoryId);
            }
            return View(Products);
        }

        // GET: /Product/Details/5
        public IActionResult Details(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Category = Categories.FirstOrDefault(c => c.Id == product.CategoryId);
            return View(product);
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(Categories, "Id", "Name");
            return View(new Product());
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            // Kiểm tra ảnh bắt buộc phải upload
            if (product.ImageFile == null || product.ImageFile.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Vui lòng chọn ảnh cho sản phẩm và tải lên");
            }

            // Kiểm tra CategoryId có nằm trong danh sách Category không
            if (!Categories.Any(c => c.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Danh mục đã chọn không hợp lệ trong hệ thống");
            }

            if (ModelState.IsValid)
            {
                // Upload ảnh vào wwwroot/products
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "products");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString("N").Substring(0, 8) + "_" + Path.GetFileName(product.ImageFile!.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }

                product.Image = "/products/" + uniqueFileName;
                product.Id = Products.Count > 0 ? Products.Max(p => p.Id) + 1 : 1;
                Products.Add(product);

                TempData["SuccessMessage"] = "Thêm mới sản phẩm thành công!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: /Product/Edit/5
        public IActionResult Edit(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = new SelectList(Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: /Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            var existing = Products.FirstOrDefault(p => p.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            // Kiểm tra danh mục
            if (!Categories.Any(c => c.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Danh mục đã chọn không hợp lệ");
            }

            if (ModelState.IsValid)
            {
                // Nếu người dùng chọn ảnh mới để thay thế
                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "products");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString("N").Substring(0, 8) + "_" + Path.GetFileName(product.ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }

                    existing.Image = "/products/" + uniqueFileName;
                }

                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.SalePrice = product.SalePrice;
                existing.Description = product.Description;
                existing.CategoryId = product.CategoryId;

                TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";
                return RedirectToAction(nameof(Index));
            }

            product.Image = existing.Image;
            ViewBag.CategoryId = new SelectList(Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: /Product/Delete/5
        public IActionResult Delete(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            product.Category = Categories.FirstOrDefault(c => c.Id == product.CategoryId);
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                Products.Remove(product);
                TempData["SuccessMessage"] = "Đã xóa sản phẩm thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
