using System;
using System.Collections.Generic;

namespace ShopBase.Data
{
    public partial class UnicalLog
    {
        public Guid Id { get; set; }
        public string LogId { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Text { get; set; } = null!;
    }
}
