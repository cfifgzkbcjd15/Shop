using System;
using System.Collections.Generic;

namespace Search.Data
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public string? Path { get; set; }

        public virtual Category Category { get; set; } = null!;
    }
}
