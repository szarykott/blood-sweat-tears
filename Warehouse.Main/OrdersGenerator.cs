using AutoFixture;
using Warehouse.Main.Catalog;
using Warehouse.Main.CustomerService;

namespace Warehouse.Main;

public class OrdersGenerator
{
    private readonly ICatalogService _catalogService;
    private readonly Fixture _fixture;
    
    public OrdersGenerator (ICatalogService catalogService)
    {
        _catalogService = catalogService;
        _fixture = new Fixture();
    }
    
    public Order GenerateOrder()
    {
        var chosenItem = _catalogService.PickRandomItem();
        var order = _fixture.Create<Order>();
        order.ProductId = chosenItem.ProductId;
        return order;
    }
}