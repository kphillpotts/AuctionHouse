using AuctionHouse.DataObjects;
using AuctionHouse.DataStore.Abstractions;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.DataStore.Azure.Stores
{
    public class AuctionStore : BaseStore<Auction>, IAuctionStore
    {
        public async override Task<IEnumerable<Auction>> GetItemsAsync(bool forceRefersh = false)
        {
            await SyncAsync();
            var items =  await StoreManager.AuctionTable.ToListAsync();
            return items;
            //IMobileServiceTable<Auction> auctionTable = StoreManager.MobileService.GetTable<Auction>();

            //return await auctionTable.ToListAsync();
        }

        public async override Task<bool> InsertAsync(Auction item)
        {
            await StoreManager.AuctionTable.InsertAsync(item);
            await SyncAsync();
            return true;
        }

        public async override Task<Auction> GetItemAsync(string Id)
        {
            IMobileServiceTable<Auction> auctionTable = StoreManager.MobileService.GetTable<Auction>();
            var auction = await auctionTable.LookupAsync(Id);

            return auction;
        }

        public async override Task<bool> SyncAsync()
        {
            await StoreManager.AuctionTable.PullAsync("auctions", StoreManager.AuctionTable.CreateQuery());
            await StoreManager.MobileService.SyncContext.PushAsync();
            return true;
        }

    }
}
