using AuctionHouse.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse.DataStore.Abstractions;
using AuctionHouse.DataStore.Mock.Helpers;

namespace AuctionHouse.DataStore.Mock.Stores
{
    public class AuctionStore : BaseStore<Auction>, IAuctionStore
    {

        List<Auction> auctions;

        public async override Task<Auction> GetItemAsync(string Id)
        {
            if (!initialized) await InitializeStore();

            return auctions.FirstOrDefault(s => s.Id == Id);
        }

        public async override Task<IEnumerable<Auction>> GetItemsAsync(bool forceRefersh = false)
        {
            if (!initialized) await InitializeStore();

            return auctions as IEnumerable<Auction>;

        }

        bool initialized = false;
        public async override Task InitializeStore()
        {
            if (initialized) return;

            auctions = new List<Auction>();

            // some random data
            List<string> locations = new List<string> { "Melbourne", "Sydney", "New York", "London" };

            for (int i = 0; i < 3; i++)
            {
                Auction auction = new Auction();
                auction.AuctionDate = DateTime.Now.AddDays(i);
                auction.Location = locations.SelectRandom();
                auctions.Add(auction);
            }

            initialized = true;
        }

    }
}
