using SalesTaxesSystem.DAL.Definitions;
using SalesTaxesSystem.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace SalesTaxesSystem.DAL.Repositories
{
    public class ItemRepository : IItemRepository
    {
        public ICollection<Item> Get()
        {
            string jsonString = "";
            string itemsFileName = "items.json";

            jsonString = File.ReadAllText(itemsFileName);
            var itemList = JsonSerializer.Deserialize<List<Item>>(jsonString);
            return itemList;
        }
    }
}
