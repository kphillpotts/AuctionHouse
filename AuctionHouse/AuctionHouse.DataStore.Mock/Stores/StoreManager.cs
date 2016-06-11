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
                if (!isInitialized)
                    InitializeAsync();

                return auctionItemStore; 
            }
        }

        public IAuctionStore AuctionStore
        {
            get
            {
                if (!isInitialized)
                    InitializeAsync();

                return auctionStore;
            }
        }

        bool isInitialized = false;

        public bool IsInitialized
        {
            get
            {
                return isInitialized;
            }
        }

        public  async Task InitializeAsync()
        {
            if (isInitialized) return;
            auctionStore = new Mock.Stores.AuctionStore();
            auctionItemStore = new Mock.Stores.AuctionItemStore(auctionStore);
            isInitialized = true;
        }
    }
}
