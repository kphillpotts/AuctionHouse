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
    public class AuctionItemStore : BaseStore<AuctionItem>, IAuctionItemStore
    {
        public async override Task<IEnumerable<AuctionItem>> GetItemsAsync(bool forceRefersh = false)
        {
            await SyncAsync();
            var items = await StoreManager.AuctionItemTable.ToListAsync();
            return items;
        }

        public async override Task<bool> InsertAsync(AuctionItem item)
        {
            await StoreManager.AuctionItemTable.InsertAsync(item);
            await SyncAsync();
            return true;
        }

        public async override Task<AuctionItem> GetItemAsync(string Id)
        {
            IMobileServiceTable<AuctionItem> auctionItemTable = StoreManager.MobileService.GetTable<AuctionItem>();
            var auction = await auctionItemTable.LookupAsync(Id);

            return auction;
        }

        public async override Task<bool> SyncAsync()
        {
            await StoreManager.AuctionTable.PullAsync("auctionItems", StoreManager.AuctionItemTable.CreateQuery());
            await StoreManager.MobileService.SyncContext.PushAsync();
            return true;
        }

        public async Task<IEnumerable<AuctionItem>> GetItemsForAuctionAsync(string auctionid)
        {
            await SyncAsync();

            var result = from auctionItem in StoreManager.AuctionItemTable where auctionItem.AuctionId == auctionid select auctionItem;
            return await result.ToListAsync();
        }
    }
}
