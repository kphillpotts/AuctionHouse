using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse.DataObjects;

namespace AuctionHouse.DataStore.Abstractions
{
    public interface IAuctionItemStore : IBaseStore<AuctionItem>
    {
        Task<IEnumerable<AuctionItem>> GetItemsForAuctionAsync(string auctionid);
    }
}
