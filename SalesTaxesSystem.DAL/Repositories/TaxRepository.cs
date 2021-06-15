using SalesTaxesSystem.DAL.Definitions;
using SalesTaxesSystem.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace SalesTaxesSystem.DAL.Repositories
{
    public class TaxRepository : ITaxRepository
    {
        public ICollection<Tax> Get()
        {
            string jsonString = "";
            string itemsFileName = "taxes.json";

            jsonString = File.ReadAllText(itemsFileName);
            var taxList = JsonSerializer.Deserialize<List<Tax>>(jsonString);
            return taxList;
        }
    }
}
