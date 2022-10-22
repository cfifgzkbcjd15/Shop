using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShopBase.Data
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}