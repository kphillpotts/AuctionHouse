using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.DataStore.Abstractions
{
    public interface IStoreManager
    {
        bool IsInitialized { get; set; }
        Task InitializeAsync();
        IAuctionStore AuctionStore { get; }
        IAuctionItemStore AuctionItemStore {get;}
    }
}
