using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.DataStore.Abstractions
{
    public interface IBaseStore<T>
    {
        Task InitializeStore();
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefersh = false);
        Task<T> GetItemAsync(string Id);
        Task<bool> InsertAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> RemoveItemAsync(T item);
        Task<bool> SyncAsync();
        void DropTable();
        string Identifier { get; }

    }
}
