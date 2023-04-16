using System;

namespace Warehouse.Main.CustomerService;

public sealed class Order
{
    public Guid ProductId { get; set; }
    public string ShippingAddress { get; set; }
}