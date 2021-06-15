using SalesTaxesSystem.BLL.Services.Contracts;
using SalesTaxesSystem.DAL.Definitions;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesTaxesSystem.BLL.Services
{
    public class CartService : ICartService
    {
        public CartService()
        {

        }

        public Cart AddItem(Cart cart, Item item, int quantity)
        {
            if (cart == null || item == null)
                return cart;

            cart.Items.Add(new Item(item, quantity));
            return cart;
        }

        public void CheckOut(Cart cart, IList<Tax> taxes)
        {
            var table = new Table();
            table.AddColumn(new TableColumn("Description"));
            table.AddColumn(new TableColumn("Price"));
            table.AddColumn(new TableColumn("Quantity"));


            cart.Items.ToList().ForEach(item =>
            {
                //TODO: Calculate taxes before showing price

                var itemPrice = (item.Price * item.Quantity).ToString() + (item.Quantity > 1 ? $"({item.Quantity} @ {item.Price})" : ""); 
                table.AddRow($"{item.Name}:", itemPrice, item.Quantity.ToString()); 
            });
            AnsiConsole.Render(table);
        }
    }
}
