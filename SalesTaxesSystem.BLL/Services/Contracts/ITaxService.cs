using SalesTaxesSystem.DAL.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxesSystem.BLL.Services.Contracts
{
    public interface ITaxService
    {
        void CalculateTaxes(IList<Tax> taxes, IList<Item> items);
        float CalculateTax(float price, float taxValue);
    }
}
