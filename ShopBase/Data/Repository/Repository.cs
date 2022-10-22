using Microsoft.EntityFrameworkCore;
using ShopBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            var count = 0;
            foreach(var item in filter.Text)
            {
                var model = db.Products.Where(x => x.Name.Contains(item)).ToList();
                if (model != null)
                {
                    count = model.Count();
                    if (count >= 10)
                    {
                        data.AddRange(model);
                        break;
                    }
                    else
                    {
                        model= db.Products.Include(x => x.Category).Where(x => x.Category.Name.Contains(item)).ToList();
                        if (model != null)
                        {
                            data.AddRange(model);
                        }
                    }
                    
                }
            }
            return data.Skip(filter.Page * pageSize).Take(filter.Page * pageSize).ToList();
        }
    }
}

