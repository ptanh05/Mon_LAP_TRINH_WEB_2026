using Day03.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day03.Controllers
{
    [Route("san-pham")]
    public class ProductController : Controller
    {
        private List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category { Id = 1, Name = "Quần Áo" },
                new Category { Id = 2, Name = "Túi xách" },
                new Category { Id = 3, Name = "Đồng hồ" },
                new Category { Id = 4, Name = "Ti vi" },
                new Category { Id = 5, Name = "Tủ lạnh" },
                new Category { Id = 6, Name = "Máy bơm" },
                new Category { Id = 7, Name = "Quạt điện" },
                new Category { Id = 8, Name = "Lò sưởi" }
            };
        }

        private List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Bộ đồ bơi cho trẻ em nam",
                    Image = "/images/Avatar/BikesHelmets83.png",
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde lure doloribus natus expedita, laborum blanditiis quaerat repellendus necessitatibus nam quo earum ex suscipit.",
                    Status = "Còn hàng",
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },
                new Product
                {
                    Id = 2,
                    Name = "Bộ đồ bơi cho trẻ em nữ",
                    Image = "/images/Avatar/BikesHelmets84.png",
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde lure doloribus natus expedita, laborum blanditiis quaerat repellendus necessitatibus nam quo earum ex suscipit.",
                    Status = "Còn hàng",
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },
                new Product
                {
                    Id = 3,
                    Name = "Bộ đồ bơi cho trẻ em từ 3-5 tuổi",
                    Image = "/images/Avatar/BikesHelmets85.png",
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde lure doloribus natus expedita, laborum blanditiis quaerat repellendus necessitatibus nam quo earum ex suscipit.",
                    Status = "Còn hàng",
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },
                new Product
                {
                    Id = 4,
                    Name = "Bộ đồ bơi cho trẻ em thời trang",
                    Image = "/images/Avatar/BikesHelmets86.png",
                    Price = 52000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde lure doloribus natus expedita, laborum blanditiis quaerat repellendus necessitatibus nam quo earum ex suscipit.",
                    Status = "Còn hàng",
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },
                new Product
                {
                    Id = 5,
                    Name = "Túi thời trang mẫu mới 2021",
                    Image = "/images/Avatar/BikesHelmets88.png",
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 2,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde lure doloribus natus expedita, laborum blanditiis quaerat repellendus necessitatibus nam quo earum ex suscipit.",
                    Status = "Còn hàng",
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },
                new Product
                {
                    Id = 6,
                    Name = "Túi thời trang da cá sấu",
                    Image = "/images/Avatar/BikesHelmets83.png",
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 2,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde lure doloribus natus expedita, laborum blanditiis quaerat repellendus necessitatibus nam quo earum ex suscipit.",
                    Status = "Còn hàng",
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                }
            };
        }

        [HttpGet("")]
        [HttpGet("~/Product")]
        [HttpGet("~/Product/Index")]
        public IActionResult Index(int? categoryId)
        {
            var categories = GetCategories();
            var products = GetProducts();

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
            }

            var viewModel = new ProductViewModel
            {
                Products = products,
                Categories = categories,
                SelectedCategoryId = categoryId
            };

            return View(viewModel);
        }

        [HttpGet("chi-tiet/{id}")]
        [HttpGet("~/Product/Detail/{id}")]
        public IActionResult Detail(int id)
        {
            var product = GetProducts().FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
