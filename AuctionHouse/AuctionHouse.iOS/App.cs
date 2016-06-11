using AuctionHouse.DataStore.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionHouse.iOS
{
    public static class App
    {

        static bool isMock = true;

        public static IStoreManager StoreManager { get; set; }

        static App()
        {
            if (isMock)
                StoreManager = new AuctionHouse.DataStore.Mock.Stores.StoreManager();
        }
    }
}
