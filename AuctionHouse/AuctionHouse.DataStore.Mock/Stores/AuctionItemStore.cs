using AuctionHouse.DataObjects;
using AuctionHouse.DataStore.Abstractions;
using AuctionHouse.DataStore.Mock.Helpers;
using AuctionHouse.DataStore.Mock.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.DataStore.Mock.Stores
{
    public class AuctionItemStore : BaseStore<AuctionItem>, IAuctionItemStore
    {
        List<AuctionItem> auctionItems;
        IAuctionStore auctionStore;

        public AuctionItemStore(IAuctionStore store)
        {
            auctionStore = store;
        }

        public async Task<IEnumerable<AuctionItem>> GetItemsForAuctionAsync(string auctionid)
        {
            if (!isInitialized) await InitializeStore();

            var result = from auctionItem in auctionItems where auctionItem.AuctionId == auctionid select auctionItem;
            return result;
        }

        public async override Task<IEnumerable<AuctionItem>> GetItemsAsync(bool forceRefersh = false)
        {
            if(!isInitialized) await InitializeStore();
            return auctionItems;
        }

        public async override Task<AuctionItem> GetItemAsync(string Id)
        {
            if (!isInitialized) await InitializeStore();
            return auctionItems.FirstOrDefault(s => s.Id == Id);
        }

        bool isInitialized = false;

        Random random = new Random();

        public async override Task InitializeStore()
        {
            if (isInitialized) return;

            auctionItems = new List<AuctionItem>();

            List<string> condition = new List<string> { "new", "used", "broken", "only driven once" };

            // generate items for each auction
            foreach (var auction in await auctionStore.GetItemsAsync())
            {
                var numItems = random.Next(100, 300);
                for (int i = 0; i < numItems; i++)
                {
                    AuctionItem item = new AuctionItem();
                    item.Id = Guid.NewGuid().ToString();
                    item.LotNumber = i;
                    item.LowerEstimate = random.Next(100, 10000);
                    item.UpperEstimate = item.LowerEstimate * 1.5m;
                    item.Description = "Some Description for item " + i;
                    item.Details = "A bunch of details for item " + i;
                    item.AuctionId = auction.Id;
                    item.Condition = condition.SelectRandom();

                    // add to mock collection
                    auctionItems.Add(item);
                }
            }
            
        }
    }
}
