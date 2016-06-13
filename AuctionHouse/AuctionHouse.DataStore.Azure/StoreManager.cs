using AuctionHouse.DataObjects;
using AuctionHouse.DataStore.Abstractions;
using AuctionHouse.DataStore.Azure.Stores;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.DataStore.Azure
{
    public class StoreManager : IStoreManager
    {
        public static MobileServiceClient MobileService { get; set; }
        private AuctionStore auctionStore;
        private AuctionItemStore auctionItemStore;

        public static IMobileServiceSyncTable<Auction> AuctionTable { get; set; }
        public static IMobileServiceSyncTable<AuctionItem> AuctionItemTable { get; set; }

        public IAuctionItemStore AuctionItemStore
        {
            get
            {
                if (!IsInitialized)
                    Initialize();

                return auctionItemStore;
            }
        }

        public IAuctionStore AuctionStore
        {
            get
            {
                if (!IsInitialized)
                    Initialize();

                return auctionStore;
            }
        }

        public bool IsInitialized { get; set; }



        public void Initialize()
        {
            if (IsInitialized) return;
            auctionStore = new AuctionStore();
            auctionItemStore = new Stores.AuctionItemStore();

            MobileService = new MobileServiceClient("http://auctionhouseserver.azurewebsites.net");

            const string path = "syncstore.db";
            var store = new MobileServiceSQLiteStore(path);
            //store.DefineTable(await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler()));
            store.DefineTable<Auction>();
            store.DefineTable<AuctionItem>();
            MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            AuctionTable = MobileService.GetSyncTable<Auction>();
            AuctionItemTable = MobileService.GetSyncTable<AuctionItem>();

            IsInitialized = true;
        }

        Task IStoreManager.InitializeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
