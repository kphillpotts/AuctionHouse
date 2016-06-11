using Foundation;
using System;
using UIKit;
using System.Linq;
using System.Collections.Generic;
using AuctionHouse.DataObjects;

namespace AuctionHouse.iOS
{
    public partial class AuctionItemViewController : UITableViewController
    {
        public AuctionItemViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            GetData();
        }

        private async void GetData()
        {
            var itemList = await App.StoreManager.AuctionItemStore.GetItemsForAuctionAsync(Auction.Id);
            items = itemList.ToList();
        }

        public Auction Auction { get; set; }
        List<AuctionHouse.DataObjects.AuctionItem> items;

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return items.Count;
        }

        const string ItemCell = "ItemCell";

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(ItemCell);
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, ItemCell);
            }

            AuctionItem item = items[indexPath.Row];

            cell.TextLabel.Text = item.Description;
            cell.DetailTextLabel.Text = item.Details;
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
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