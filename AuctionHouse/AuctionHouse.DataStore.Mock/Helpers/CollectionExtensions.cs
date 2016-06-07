using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.DataStore.Mock.Helpers
{
    public static class CollectionExtensions
    {
        static Random rnd = new Random();

        public static T SelectRandom<T>(this List<T> list) => list[rnd.Next(list.Count)];

    }
}
