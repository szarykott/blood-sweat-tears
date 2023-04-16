using System;

namespace Warehouse.Main.Storage;

public sealed class Product
{
    public Guid Id { get; init; }
    
    // simulates large object
    private byte[] _veryImportantData = new byte[100];
}