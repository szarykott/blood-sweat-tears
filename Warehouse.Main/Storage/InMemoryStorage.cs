using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Main.Items;

namespace Warehouse.Main.Storage;

internal sealed class InMemoryStorage : IStorageSystem
{
    private readonly List<Product> _storageItems;
    private readonly List<Product> _reservedItems;

    public InMemoryStorage ()
    {
        _storageItems = new List<Product>();
        _reservedItems = new List<Product>();
    }
    
    public IDisposable IngestItemsStream(IObservable<Item> items)
    {
        return items.Subscribe(
            onNext: AddProduct, 
            onCompleted: () => Console.WriteLine($"{nameof(IngestItemsStream)} finished"),
            onError: exception => throw exception);
    }

    private void AddProduct(Item item)
    {
        lock (_storageItems)
        {
            _storageItems.Add(item.ToProduct());
        }
    }

    public IEnumerable<Product> ListAvailable()
    {
        lock (_storageItems)
        {
            return _storageItems.Select(x => x).ToList();
        }
    }

    public bool Reserve(Guid id)
    {
        lock (_storageItems)
        {
            for (var i = 0; i < _storageItems.Count; i++)
            {
                if (_storageItems[i].Id == id)
                {
                    var product = _storageItems[i];
                    _storageItems.RemoveAt(i);
                    _reservedItems.Add(product);
                    return true;
                }
            }
        }

        return false;
    }

    public Product? Get(Guid id)
    {
        lock (_reservedItems)
        {
            for (var i = 0; i < _reservedItems.Count; i++)
            {
                if (_reservedItems[i].Id == id)
                {
                    var product = _reservedItems[i];
                    _reservedItems.RemoveAt(i);
                    return product;
                }
            }
        }

        return null;
    }
}