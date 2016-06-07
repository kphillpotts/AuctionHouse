using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionHouse.DataObjects
{
    public class Auction
    {
        public string Id { get; set; }

        public DateTime AuctionDate { get; set; }

        public string Location { get; set; }

    }
}
