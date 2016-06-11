using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using System.Linq;
using Foundation;
using AuctionHouse.DataObjects;

namespace AuctionHouse.iOS
{
    public class AuctionViewController : UITableViewController
    {
        public string Auction { get; set; }
        List<AuctionHouse.DataObjects.AuctionItem> items;

        public AuctionViewController(string AuctionId)
        {
            this.Auction = AuctionId;
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.Title = "Items";

            var itemList = await App.StoreManager.AuctionItemStore.GetItemsForAuctionAsync(this.Auction);
            items = itemList.ToList();
        }

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
    }
}
