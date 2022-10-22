using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBase.Data;
using ShopBase.Data.Repository;
using ShopBase.Models;

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
        [HttpPost]
        public List<Product> GetProducts(FilterModel filter)
        {
            var data = new List<Product>();
            var pageSize = 10;
            var count = 0;
            //.Skip(filter.Page * pageSize).Take(filter.Page * pageSize)
            foreach (var item in filter.Text)
            {
                var model = db.Products.Where(x => x.Name.ToLower().Contains(item.ToLower())).ToList();
                if (model != null)
                {
                    count = model.Count();
                    data.AddRange(model);
                    if (count >= 10)
                    {
                        return data.Skip(filter.Page * pageSize).Take(pageSize).ToList();
                    }
                    else
                    {
                        model = db.Products.Include(x => x.Category).Where(x => x.Category.Name.ToLower().Contains(item.ToLower())).ToList();
                        if (model != null)
                        {
                            data.AddRange(model);
                        }
                    }

                }
            }
            return data.Skip(filter.Page * pageSize).Take(pageSize).ToList();
        }
    }
}
