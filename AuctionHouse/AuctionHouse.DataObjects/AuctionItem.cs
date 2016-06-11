using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionHouse.DataObjects
{
    public class AuctionItem
    {
        public string Id { get; set; }

        public string AuctionId { get; set; }

        public int LotNumber { get; set; }

        public string Description { get; set; }

        public decimal LowerEstimate { get; set; }

        public decimal UpperEstimate { get; set; }

        public string Details { get; set; }

        public string Condition { get; set; }

        public string Image { get; set; }

    }
}
