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
        private readonly ITaxService _taxService;

        public CartService(ITaxService taxService)
        {
            _taxService = taxService;
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

            var grid = new Grid();
            grid.AddColumn(new GridColumn());
            grid.AddColumn(new GridColumn());
            grid.AddColumn(new GridColumn());

            grid.AddRow("Description", "Price", "Quantity");


            _taxService.CalculateTaxes(taxes, cart.Items.ToList());


            cart.Items.ToList().ForEach(item =>
            {
                var itemPrice = (item.Price * item.Quantity).ToString() + (item.Quantity > 1 ? $"({item.Quantity} @ {item.Price})" : ""); 

                //TODO: Calculate taxes before showing price
                table.AddRow($"{item.Name}:", itemPrice, item.Quantity.ToString());
                grid.AddRow($"{item.Name}:", itemPrice, item.Quantity.ToString());
            });
            AnsiConsole.Render(grid);
        }
    }
}
