using SalesTaxesSystem.DAL.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxesSystem.DAL.Repositories.Contracts
{
    public interface IItemRepository
    {
        ICollection<Item> Get();
    }
}
