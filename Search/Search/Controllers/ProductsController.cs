using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Search.Models;
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
        public List<ProductViewModel> GetProducts([FromQuery] string text, [FromQuery] int page, [FromQuery] bool hite)
        {
            //var pageSize = 10;
            //var product = new List<Product>();
            //var textMas = text.Split(" ").Where(x => x != null && x != "").ToList();
            //string ss = "";
            //ss = textMas[0];
            //var firstProduct = db.Products
            //    .Where(x => x.Name.ToLower().Contains(text.ToLower()))
            //    .Select(x => new Product { Id = x.Id, Name = x.Name })
            //    .ToList();

            //if (firstProduct != null && firstProduct.Count() > 0)
            //{
            //    product.AddRange(firstProduct);
            //}
            //foreach (var item in textMas)
            //{
            //    var model = db.Products
            //        .Where(x => x.Name.ToLower().Contains(item.ToLower()))
            //        .Select(x => new Product { Id = x.Id, Name = x.Name })
            //        .ToList();
            //    if (model != null)
            //    {
            //        product.AddRange(model);
            //    }
            //}
            //foreach (var item in textMas)
            //{
            //    var model = db.Products
            //        .Include(x => x.Category)
            //        .Where(x => x.Category.Name.ToLower().Contains(item.ToLower()))
            //        .Select(x => new Product { Id = x.Id, Name = x.Name })
            //        .ToList();
            //    if (model != null)
            //    {
            //        product.AddRange(model);
            //    }
            //}
            //foreach (var item in db.Categories.ToList())
            //{
            //    if (text.ToLower().Contains(item.Name.ToLower()))
            //    {
            //        product.AddRange(db.Products.Where(x => x.CategoryId == item.Id)
            //            .Select(x => new Product { Id = x.Id, Name = x.Name })
            //            .ToList());

            //    }

            //}
            //foreach (var item in textMas)
            //{
            //    var newText = item.Substring(0, item.Length - 1);
            //    product.AddRange(
            //        db.Products
            //        .Include(x => x.Category)
            //        .Where(x => x.Category.Name.ToLower().Contains(newText.ToLower()) || x.Name.ToLower().Contains(newText.ToLower()))
            //        .Select(x=>new Product { Id=x.Id, Name=x.Name })
            //        .ToList()
            //        );
            //}
            //return product
            //    .DistinctBy(x => x.Name)
            //    .Skip(pageSize * page)
            //    .Take(pageSize)
            //    .Select(x => new Product { Id = x.Id, Name = x.Name })
            //    .ToList();
            var pageSize = 10;
            var products = new List<ProductViewModel>();
            //textMas=text.Split(" ").Where(x => x != null && x != "").ToList()
            if (hite)
            {
                return db.Products
                    .Where(x => x.Name.ToLower().Contains(text.ToLower()))
                    .Skip(pageSize * page).Take(pageSize)
                    .Select(x => new ProductViewModel { Name = x.Name })
                    .ToList();
            }
            else
            {
                string[] predlogs = { "без", "близ", "в", "вместо", "вне", "во", "безо", "для", "до", "за", "из", "изо", "из-за", "из-под", "к", "ко", "кроме", "между", "меж", "на", "над", "надо", "о", "об", "обо", "от", "ото", "перед", "по", "под", "при", "про", "ради", "c", "среди", "у", "через" };
                List<string> textMas = text.Split(" ").Where(x => x != null && x != "").ToList();
                foreach (var item in text.Split(" ").Where(x => x != null && x != "").ToList())
                {
                    foreach (var predlog in predlogs)
                    {
                        if (item.ToLower() == predlog.ToLower())
                            textMas.Remove(textMas.Where(x => x == predlog).FirstOrDefault());
                    }
                    text = item.Remove(item.Length - 1);
                }

                var newMas = new List<string>();
                foreach (var item in textMas)
                {
                    if (item.Length > 3)
                    {
                        newMas.Add(item.Remove(item.Length - 1));
                    }
                    else
                    {
                        newMas.Add(item);
                    }
                }
                text = string.Join(" ", newMas);
                var product = new List<ProductViewModel>();

                foreach (var category in db.Categories.ToList())
                {
                    foreach (var item in newMas)
                    {
                        if (category.Name.ToLower().Contains(item.ToLower()))
                        {
                            
                            if (newMas.Count() > 1)
                            {
                                products = db.Products
                                .Where(x =>
                                x.CategoryId == category.Id
                                && x.Name.ToLower().Contains(newMas[1].ToLower()) && x.Name.ToLower().Contains(newMas[0].ToLower()))
                                //.Where(x => x.Name.ToLower().Contains(item.ToLower()))
                                .Skip(pageSize * page)
                                .Take(pageSize)
                                .Select(x => new ProductViewModel { Name = x.Name })
                                .ToList();
                                if (products != null)
                                {
                                    product.AddRange(products);
                                }
                            }
                            else
                            {
                                products = db.Products
                                .Where(x => x.CategoryId == category.Id && x.Name.ToLower().Contains(item.ToLower())).Skip(pageSize * page)
                                .Take(pageSize).Select(x => new ProductViewModel { Name = x.Name }).ToList();
                                if (products != null)
                                {
                                    product.AddRange(products);
                                }
                            }

                            //var fullProduct = db.Products
                            //    .Where(x => x.Name.ToLower().Contains(text.ToLower())).Skip(pageSize * page)
                            //    .Take(pageSize).Select(x => new ProductViewModel { Name = x.Name }).ToList();

                            //if (fullProduct != null)
                            //{
                            //    products.AddRange(fullProduct
                            //       .Select(x => new ProductViewModel { Name = x.Name })
                            //       .ToList()
                            //       );
                            //}
                            //if (product != null)
                            //{
                            //    products.AddRange(product
                            //        .Select(x => new ProductViewModel { Name = x.Name })
                            //        .ToList()
                            //        );
                            //}

                        }
                        else
                        {
                            products = db.Products
                                .Where(x => x.Name.ToLower().Contains(item.ToLower()))
                                .Skip(pageSize * page)
                                .Take(pageSize)
                                .Select(x => new ProductViewModel { Name = x.Name })
                                .ToList();
                            if (products != null)
                            {
                                product.AddRange(product
                                    .Select(x => new ProductViewModel { Name = x.Name })
                                    .ToList()
                                    );
                            }
                        }
                        if (product.Count() >= pageSize)
                        {
                            return product
                                .Skip(pageSize * page).Take(pageSize).DistinctBy(x => x.Name)
                                .ToList();
                        }
                    }
                }
                return product
                    .Skip(pageSize * page).Take(pageSize)
                    .DistinctBy(x => x.Name).ToList();
            }
        }
        [HttpGet("GetSearchString")]
        public async Task<List<Product>> GetSearchString(string text)
        {
            var firstProduct = await db.Products.Where(x => x.Name.ToLower().Contains(text.ToLower())).Take(10).Select(x => new Product
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            if (firstProduct != null && firstProduct.Count() > 0)
            {
                return firstProduct;
            }
            else
                return null;
        }

    }
}
