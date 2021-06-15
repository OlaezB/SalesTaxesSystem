using SalesTaxesSystem.BLL.Services.Contracts;
using SalesTaxesSystem.DAL.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesTaxesSystem.BLL.Services
{
    public class TaxService : ITaxService
    {
        public void CalculateTaxes(IList<Tax> taxes, IList<Item> items)
        {
            var taxList = taxes.ToList();
            var itemList = items.ToList();
            taxList.ForEach(tax => {
                var applicableItemsForTax = itemList.Where(item => {
                    if (tax.Classes.Contains("All"))
                        return true;

                    return item.Classes.Intersect(tax.Classes).Any();
                    });
                applicableItemsForTax.ToList()
                .ForEach(item => item.Price = CalculateTax(item.Price, tax.Value));
            });

            return;
        }

        public float CalculateTax (float price, float taxValue)
        {
            var tax = price * taxValue;
            var taxRounded = (float) Math.Ceiling(tax * 20) / 20;
            var total = price + taxRounded;

            return total;
        }
    }
}
