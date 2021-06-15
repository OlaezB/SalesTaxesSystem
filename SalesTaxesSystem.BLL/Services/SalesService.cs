using System.Text.Json;
using SalesTaxesSystem.BLL.Services.Contracts;
using SalesTaxesSystem.DAL.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Spectre.Console;
using System.Collections;
using SalesTaxesSystem.DAL.Repositories;
using SalesTaxesSystem.DAL.Repositories.Contracts;

namespace SalesTaxesSystem.BLL.Services
{
    public class SalesService : ISalesService
    {
        private Cart cart;
        private IList<Item> itemCatalog;
        private IList<Tax> taxList;
        private readonly ICartService _cartService;
        private readonly IItemRepository _itemRepository;
        private readonly ITaxRepository _taxRepository;

        public SalesService(ICartService cartService, IItemRepository itemRepository, ITaxRepository taxRepository)
        {
            cart = new Cart();
            itemCatalog = new List<Item>();
            taxList = new List<Tax>();
            _cartService = cartService;
            _itemRepository = itemRepository;
            _taxRepository = taxRepository;
        }

        public void SetUp()
        {
            itemCatalog = _itemRepository.Get().ToList();
            taxList = _taxRepository.Get().ToList();

            PrintCatalog(itemCatalog);

            var input = "";

            do
            {
                long itemId;
                int quantity;
                input = Console.ReadLine();
                if (!long.TryParse(input, out itemId))
                {
                    PrintError($"Thats not a valid input, enter either an item Id, [[e]] to exit or [[c]] to checkout");
                    continue;
                }

                var item = itemCatalog.FirstOrDefault(i => i.Id == itemId);
                if(item == null)
                {
                    PrintError($"Id {itemId} not found in catalog, check if you entered it right or try another item");
                    continue;
                }

                AnsiConsole.MarkupLine($"How many of {item.Name} do you want to add to your cart?");
                input = Console.ReadLine();

                if (!int.TryParse(input, out quantity))
                {
                    PrintError($"Thats not a valid input, enter either an integer number, [[e]] to exit or [[c]] to checkout");
                    continue;
                }

                _cartService.AddItem(cart, item, quantity);


            } while (input != "e" && input != "c");

            if (input == "e")
            {
                AnsiConsole.Render(new Rule("[yellow]test [/]").RuleStyle("grey").LeftAligned());
                return;
            }

            _cartService.CheckOut(cart, taxList);
        }

        public void PrintCatalog(ICollection<Item> items)
        {
            AnsiConsole.MarkupLine("Add your products by typing the Id and when prompted the quantity");
            var table = new Table()
                .AddColumn(new TableColumn("ID"))
                .AddColumn(new TableColumn("Item Name"))
                .AddColumn(new TableColumn("Price"))
                .AddColumn(new TableColumn("Categories"));

            items.ToList().ForEach(item =>
            {
                table.AddRow(item.Id.ToString(), item.Name, item.Price.ToString(), String.Join(",", item.Classes));
            });
            AnsiConsole.Render(table);
            
            return;
        }

        void SearchItem(long itemId)
        {
            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                return;
        }

        void PrintError(string errorMsg)
        {
            AnsiConsole.Clear();
            PrintCatalog(itemCatalog);
            AnsiConsole.MarkupLine(errorMsg);
        }

    }
}
