using Foundation;
using System;
using UIKit;
using System.Linq;
using System.Collections.Generic;
using AuctionHouse.DataObjects;
using FFImageLoading;
using System.Threading.Tasks;

namespace AuctionHouse.iOS
{
    public partial class AuctionItemViewController : UITableViewController
    {
        public AuctionItemViewController (IntPtr handle) : base (handle)
        {
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            await GetData();

            // just some random code to fake up a record being added
            this.NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Add, async delegate
            {

                Random random = new Random();
                AuctionItem item = new AuctionItem();
                item.LotNumber = items.Count + 1;
                item.LowerEstimate = random.Next(100, 10000);
                item.UpperEstimate = item.LowerEstimate * 1.5m;
                item.Description = "Some Description for item " + item.LotNumber;
                item.Details = "A bunch of details for item " + item.LotNumber;
                item.AuctionId = Auction.Id;
                item.Condition = "new";
                item.Image = @"http://dumouchelle.com/lotImages/201606/1200/2016060001_1.jpg";

                await App.StoreManager.AuctionItemStore.InsertAsync(item);

                await GetData();
            });
        }

        private async Task GetData()
        {
            var itemList = await App.StoreManager.AuctionItemStore.GetItemsForAuctionAsync(Auction.Id);
            items = itemList.ToList();
            TableView.ReloadData();
        }

        public Auction Auction { get; set; }
        List<AuctionHouse.DataObjects.AuctionItem> items = new List<AuctionItem>();

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return items.Count;
        }

        const string ItemCell = "ItemCell";

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("AuctionItemCell") as AuctionItemCell;

            AuctionItem item = items[indexPath.Row];

            cell.Update(item);

            return cell;
        }


        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            this.PerformSegue("ShowDetailsSegue", this);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            var detailsVC = segue.DestinationViewController as ItemDetailsViewController;

            if (detailsVC != null)
            {
                // pass the item across
                var item = items[this.TableView.IndexPathForSelectedRow.Row];
                detailsVC.AuctionItem = item;
            }
        }
    }
}