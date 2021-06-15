using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxesSystem.DAL.Definitions
{
    public class Cart
    {
        public float Total { get; set; }
        public float SalesTaxes { get; set; }
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
