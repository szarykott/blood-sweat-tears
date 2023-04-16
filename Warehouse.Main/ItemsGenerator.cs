using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Warehouse.Main.Catalog;
using Warehouse.Main.Items;

namespace Warehouse.Main;

public class ItemsGenerator : IObservable<Item>
{
    private readonly ICatalogService _catalogService;

    public ItemsGenerator (ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }
    
    public IDisposable Subscribe(IObserver<Item> observer)
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        Task.Run(() => Generate(observer, cts.Token), cts.Token);
        return new CancelDispose(cts);
    }

    private void Generate(IObserver<Item> observer, CancellationToken cancellationToken)
    {
        var fixture = new Fixture();
        while (true)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                observer.OnCompleted();
                break;
            }
            
            observer.OnNext(RandomItem(fixture));
        }
    }

    private Item RandomItem(Fixture fixture)
    {
        var item = fixture.Create<Item>();
        var somethingFromCatalog = _catalogService.PickRandomItem();
        item.ProductId = somethingFromCatalog.ProductId;
        return item;
    }

    private class CancelDispose : IDisposable
    {
        private readonly CancellationTokenSource _cts;

        public CancelDispose (CancellationTokenSource cts)
        {
            _cts = cts;
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}