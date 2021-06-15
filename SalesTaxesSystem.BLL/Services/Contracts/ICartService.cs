using SalesTaxesSystem.DAL.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxesSystem.BLL.Services.Contracts
{
    public interface ICartService
    {
        Cart AddItem(Cart cart, Item item, int quantity);
        void CheckOut(Cart cart, IList<Tax> taxes);
    }
}
