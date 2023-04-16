using System;
using System.Threading;
using Warehouse.Main;
using Warehouse.Main.Catalog;
using Warehouse.Main.CustomerService;
using Warehouse.Main.Shipping;
using Warehouse.Main.Storage;

Console.WriteLine("Bootstrapping the system ...");

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_,  _) =>
{
    Console.WriteLine("User interrupt detected");
    cts.Cancel();
};

var catalogService = new CatalogService();

var ordersGenerator = new OrdersGenerator(catalogService);

var storage = new InMemoryStorage();
var itemsIngestion = storage.IngestItemsStream(new ItemsGenerator(catalogService));

var shippingService = new ShippingService(storage);

var customerService = new CustomerService(storage, shippingService);

Console.WriteLine("Done");
Console.WriteLine("Press CTRL+C to quit");


while (!cts.IsCancellationRequested)
{
    var nextOrder = ordersGenerator.GenerateOrder();
    var _ = customerService.HandleOrder(nextOrder);
}

itemsIngestion.Dispose();
