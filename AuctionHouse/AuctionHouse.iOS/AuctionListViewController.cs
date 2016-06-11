using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using AuctionHouse.DataObjects;
using System.Linq;
namespace AuctionHouse.iOS
{
    public partial class AuctionListViewController : UITableViewController
    {
        public AuctionListViewController (IntPtr handle) : base (handle)
        {
        }

        List<DataObjects.Auction> auctions;

        public async void GetData()
        {
            var auctionList = await App.StoreManager.AuctionStore.GetItemsAsync();
            auctions = auctionList.ToList();
        }

        const string AuctionCell = "AuctionCell";


        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view
            this.NavigationItem.Title = "Auctions";

            GetData();
        }


        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(AuctionCell);
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, AuctionCell);
            }

            Auction auction = auctions[indexPath.Row];

            cell.TextLabel.Text = auction.AuctionDate.ToShortDateString();
            cell.DetailTextLabel.Text = auction.Location;
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            Auction auction = auctions[indexPath.Row];

            this.PerformSegue("ShowItemsSegue", this);
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return auctions.Count;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            Auction auction = auctions[this.TableView.IndexPathForSelectedRow.Row];
            var itemListController = (AuctionItemViewController)segue.DestinationViewController;
            itemListController.Auction = auction;
        }

        
    }

}