using AuctionHouse.DataStore.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.DataStore.Mock.Stores
{
    public class BaseStore<T> : IBaseStore<T>
    {
        public virtual string Identifier
        {
            get
            {
                return "BaseStore";
            }
        }

        public virtual void DropTable()
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetItemAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> GetItemsAsync(bool forceRefersh = false)
        {
            throw new NotImplementedException();
        }

        public virtual Task InitializeStore()
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> InsertAsync(T item)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> RemoveItemAsync(T item)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> SyncAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}
