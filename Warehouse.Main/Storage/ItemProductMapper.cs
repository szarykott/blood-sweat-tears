using System;
using Warehouse.Main.Items;

namespace Warehouse.Main.Storage;

public static class ItemProductMapper
{
    public static Product ToProduct(this Item item)
    {
        return new Product
        {
            Id = item.ProductId,
        };
    }
}