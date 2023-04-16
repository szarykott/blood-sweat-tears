using System;
using Warehouse.Main.Shipping;
using Warehouse.Main.Storage;

namespace Warehouse.Main.CustomerService;

public class CustomerService : ICustomerService
{
    private readonly IStorageSystem _storage;
    private readonly IShippingService _shippingService;

    public CustomerService (IStorageSystem storage, IShippingService shippingService)
    {
        _storage = storage;
        _shippingService = shippingService;
    }
    
    public OrderReply HandleOrder(Order order)
    {
        var availableProducts = _storage.ListAvailable();

        foreach (var product in availableProducts)
        {
            if (product.Id == order.ProductId)
            {
                var reservationSuccessful = _storage.Reserve(product.Id);
                if (reservationSuccessful && Random.Shared.Next(0, 100) >= 30)
                {
                    _shippingService.Ship(product.Id, order.ShippingAddress);
                    return new OrderReply { Found = true };
                }
            }
        }

        return new OrderReply { Found = false };
    }
}