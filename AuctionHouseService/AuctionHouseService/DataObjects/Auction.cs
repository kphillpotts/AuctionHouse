using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuctionHouseService.DataObjects
{
    public class Auction : EntityData
    {
        public string Id { get; set; }

        public DateTime AuctionDate { get; set; }

        public string Location { get; set; }

    }
}