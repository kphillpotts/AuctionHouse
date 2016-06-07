using AuctionHouse.DataObjects;
using AuctionHouse.DataStore.Abstractions;
using AuctionHouse.DataStore.Mock.Stores;
using System;

using UIKit;

namespace AuctionHouse.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            IAuctionStore b = new AuctionHouse.DataStore.Mock.Stores.AuctionStore();
            IAuctionItemStore c = new AuctionHouse.DataStore.Mock.Stores.AuctionItemStore(b);

            var x = await c.GetItemsAsync();


        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}