using Microsoft.Extensions.Logging;
using SalesTaxesSystem.BLL.Services.Contracts;
using Serilog;
using System;

namespace SalesTaxesSystem
{
    partial class Program
    {
        class App
        {
            private readonly ILogger<App> _logger;
            private readonly ISalesService _salesService;

            public App(ILogger<App> logger, ISalesService salesService)
            {
                _logger = logger;
                _salesService = salesService;
            }

            public void Run()
            {
                //Console.WriteLine(_salesService.PrintCatalog(null));
                _salesService.SetUp();
                Console.ReadLine();
                return;
            }
        }
    }
}
