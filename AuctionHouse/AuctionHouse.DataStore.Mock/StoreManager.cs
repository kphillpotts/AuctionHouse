using AuctionHouse.DataStore.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.DataStore.Mock.Stores
{
    public class StoreManager : IStoreManager
    {
        AuctionStore auctionStore;
        AuctionItemStore auctionItemStore;

        public IAuctionItemStore AuctionItemStore
        {
            get
            {
                if (!IsInitialized)
                    InitializeAsync();

                return auctionItemStore; 
            }
        }

        public IAuctionStore AuctionStore
        {
            get
            {
                if (!IsInitialized)
                    InitializeAsync();

                return auctionStore;
            }
        }

        public bool IsInitialized { get; set; }

        public  async Task InitializeAsync()
        {
            if (IsInitialized) return;
            auctionStore = new Mock.Stores.AuctionStore();
            auctionItemStore = new Mock.Stores.AuctionItemStore(auctionStore);
            IsInitialized = true;
        }
    }
}
