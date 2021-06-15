using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxesSystem.DAL.Definitions
{
    public class Item
    {
        public Item()
        {

        }

        public Item(Item item, int quantity)
        {
            Id = item.Id;
            Name = item.Name;
            Price = item.Price;
            Quantity = quantity;
            Classes = item.Classes;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public ICollection<string> Classes { get; set; }
    }
}
