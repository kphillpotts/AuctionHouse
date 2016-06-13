using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionHouse.DataObjects
{
    public class Auction
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        public DateTime AuctionDate { get; set; }

        public string Location { get; set; }

    }
}
