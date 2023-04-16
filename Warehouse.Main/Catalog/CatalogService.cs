using System;
using System.Collections.Generic;
using AutoFixture;

namespace Warehouse.Main.Catalog;

public class CatalogService : ICatalogService
{
    private const int MaxItems = 25;
    
    private readonly List<CatalogItem> _availableItems;

    public CatalogService ()
    {
        _availableItems = new List<CatalogItem>();
        var fixture = new Fixture();
        for (var i = 0; i < MaxItems; i++)
        {
            _availableItems.Add(fixture.Create<CatalogItem>());
        }
    }

    public CatalogItem PickRandomItem()
    {
        return _availableItems[Random.Shared.Next(0, MaxItems)];
    }
}