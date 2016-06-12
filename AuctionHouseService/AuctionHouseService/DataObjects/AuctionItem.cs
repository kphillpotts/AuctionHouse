using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuctionHouseService.DataObjects
{
    public class AuctionItem : EntityData
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