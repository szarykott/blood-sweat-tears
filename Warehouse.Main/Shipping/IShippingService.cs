using System;

namespace Warehouse.Main.Shipping;

public interface IShippingService
{
    public void Ship(Guid productId, string address);
}