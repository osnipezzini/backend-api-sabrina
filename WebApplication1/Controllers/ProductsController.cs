using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebApplication1.DataModels;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpPost("/product")]
        public IActionResult CreateProduct(ProductModel model) 
        { 
            var product = Constants.Products.FirstOrDefault(x => x.Name == model.Name);

            if (product != null)
                return BadRequest("Produto já foi cadastrado");

            var category = Constants.Categories.FirstOrDefault(x => x.Id == model.Category);

            if (category == null)
                return NotFound("Categoria não encontrada");

            product = new Models.Product
            {
                Name = model.Name,
                Category = category,
                Id = Constants.Products.Max(x => x.Id) + 1,
                Image = model.Image,
                Price = model.Price,
                Quantity = model.Quantity
            };

            Constants.AddProduct(product);

            model.Id = product.Id;
            return Ok(model);
        }

        [HttpPost("/product/category")]
        public IActionResult CreateCategory(CategoryModel model)
        {
            var category = Constants.Categories.FirstOrDefault(x => x.Name == model.Name);

            if (category != null)
                return BadRequest("Categoria já foi cadastrada");

            category = new Models.Category
            {
                Id = Constants.Categories.Max(x => x.Id) + 1,
                Name = model.Name,
            };           

            Constants.AddCategory(category);

            model.Id = category.Id;
            return Ok(model);
        }

        [HttpPut("/product")]
        public IActionResult UpdateProduct(ProductModel model)
        {
            var product = Constants.Products.FirstOrDefault(x => x.Id == model.Id);

            if (product == null)
                return NotFound("Produto não encontrado");

            var category = Constants.Categories.FirstOrDefault(x => x.Id == model.Category);

            if (category == null)
                return NotFound("Categoria não encontrada");

            product = new Models.Product
            {
                Name = model.Name,
                Category = category,
                Id = model.Id,
                Image = model.Image,
                Price = model.Price,
                Quantity = model.Quantity
            };

            Constants.UpdateProduct(product);
            return Ok(model);
        }

        [HttpPut("/product/category")]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            var category = Constants.Categories.FirstOrDefault(x => x.Id == model.Id);

            if (category == null)
                return NotFound("Categoria não foi encontrada");

            category = new Models.Category
            {
                Id = Constants.Categories.Max(x => x.Id) + 1,
                Name = model.Name,
            };

            Constants.AddCategory(category);

            model.Id = category.Id;
            return Ok(model);
        }

        [HttpGet("/products")]
        public IActionResult GetProducts()
        {
            var products = Constants.Products
                .Select(x => new ProductModel
                {
                    Name = x.Name,
                    Category = x.Category.Id,
                    Id = x.Id,
                    Image = x.Image,
                    Price= x.Price,
                    Quantity = x.Quantity,
                })
                .ToList();

            return Ok(products);
        }

        [HttpGet("/products/categories")]
        public IActionResult GetCategories(CategoryModel model)
        {
            var categories = Constants.Categories.ToList();
            
            return Ok(categories);
        }
    }
}
