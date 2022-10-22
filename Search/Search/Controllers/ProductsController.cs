using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBase.Data;

namespace Search.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        ShopContext db;
        public ProductsController(ShopContext _db)
        {
            db = _db;
        }
        [HttpGet]
        public List<Product> GetProducts([FromQuery] string text, [FromQuery] int page)
        {
            var pageSize = 10;
            var product = new List<Product>();
            var textMas = text.Split(" ").Where(x => x != null && x != "").ToList();
            string ss = "";
            ss = textMas[0];
            var firstProduct = db.Products
                .Where(x => x.Name.ToLower().Contains(text.ToLower()))
                .Select(x => new Product { Id = x.Id, Name = x.Name })
                .ToList();

            if (firstProduct != null && firstProduct.Count() > 0)
            {
                product.AddRange(firstProduct);
            }
            foreach (var item in textMas)
            {
                var model = db.Products
                    .Where(x => x.Name.ToLower().Contains(item.ToLower()))
                    .Select(x => new Product { Id = x.Id, Name = x.Name })
                    .ToList();
                if (model != null)
                {
                    product.AddRange(model);
                }
            }
            foreach (var item in textMas)
            {
                var model = db.Products
                    .Include(x => x.Category)
                    .Where(x => x.Category.Name.ToLower().Contains(item.ToLower()))
                    .Select(x => new Product { Id = x.Id, Name = x.Name })
                    .ToList();
                if (model != null)
                {
                    product.AddRange(model);
                }
            }
            foreach (var item in db.Categories.ToList())
            {
                if (text.ToLower().Contains(item.Name.ToLower()))
                {
                    product.AddRange(db.Products.Where(x => x.CategoryId == item.Id)
                        .Select(x => new Product { Id = x.Id, Name = x.Name })
                        .ToList());

                }

            }
            foreach (var item in textMas)
            {
                var newText = item.Substring(0, item.Length - 1);
                product.AddRange(
                    db.Products
                    .Include(x => x.Category)
                    .Where(x => x.Category.Name.ToLower().Contains(newText.ToLower()) || x.Name.ToLower().Contains(newText.ToLower()))
                    .Select(x=>new Product { Id=x.Id, Name=x.Name })
                    .ToList()
                    );
            }
            return product
                .DistinctBy(x => x.Name)
                .Skip(pageSize * page)
                .Take(pageSize)
                .Select(x => new Product { Id = x.Id, Name = x.Name })
                .ToList();
        }
        [HttpGet("GetSearchString")]
        public async Task<List<Product>> GetSearchString(string text)
        {
            var firstProduct = await db.Products.Where(x => x.Name.ToLower().Contains(text.ToLower())).ToListAsync();
            if (firstProduct != null && firstProduct.Count() > 0)
            {
                return firstProduct;
            }
            else
                return null;
        }

    }
}
