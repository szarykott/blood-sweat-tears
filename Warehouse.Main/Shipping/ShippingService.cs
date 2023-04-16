using System;
using Warehouse.Main.Storage;

namespace Warehouse.Main.Shipping;

public sealed class ShippingService : IShippingService
{
    private readonly IStorageSystem _storageSystem;

    public ShippingService (IStorageSystem storageSystem)
    {
        _storageSystem = storageSystem;
    }
    
    public void Ship(Guid productId, string address)
    {
        try
        {
            var product = _storageSystem.Get(productId);
            // Console.WriteLine($"Shipping {product!.Id} to {address}");
        }
        catch
        {
            // ignored
        }
    }
}