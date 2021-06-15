using SalesTaxesSystem.DAL.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxesSystem.BLL.Services.Contracts
{
    public interface ISalesService
    {
        void PrintCatalog(ICollection<Item> items);
        void SetUp();
    }
}
