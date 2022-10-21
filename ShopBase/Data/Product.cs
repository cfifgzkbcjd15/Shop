using System;
using System.Collections.Generic;

namespace ShopBase.Data
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public string CodKpgz { get; set; } = null!;
        public string Specifications { get; set; } = null!;

        public virtual Category Category { get; set; } = null!;
    }
}
