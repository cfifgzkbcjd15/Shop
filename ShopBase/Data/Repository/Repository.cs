using ShopBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBase.Data.Repository
{
    public partial class Repository
    {
        private readonly ShopContext db;
        public Repository(ShopContext _db)
        {
            db = _db;
        }
        public List<Product> GetProducts(FilterModel filter)
        {
            var data=new List<Product>();
            var pageSize = 10;
            foreach(var item in filter.Text)
            {
                var model = db.Products.Where(x => x.Name == item).ToList();
                if (model != null)
                {
                    data.AddRange(model);
                }
            }
            return data;
        }
    }
}

