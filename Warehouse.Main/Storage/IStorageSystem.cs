using System;
using System.Collections.Generic;

namespace Warehouse.Main.Storage;

public interface IStorageSystem
{
    /// <summary>
    /// Lists all of storage contents, but without transferring ownership of any of them 
    /// </summary>
    IEnumerable<Product> ListAvailable();
    
    /// <summary>
    /// Reserves an item in the storage for the shipping system
    /// </summary>
    /// <returns>Boolean indicating if reservation was successful</returns>
    bool Reserve(Guid id);


    /// <summary>
    /// Retrieves a product from the storage 
    /// </summary>
    Product? Get(Guid id);
}