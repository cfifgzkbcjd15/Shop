using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShopBase.newData
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}