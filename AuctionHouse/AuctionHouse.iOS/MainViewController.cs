using System;
using System.Drawing;
using CoreFoundation;
using UIKit;
using Foundation;
using System.Collections.Generic;
using System.Linq;
using AuctionHouse.DataObjects;

namespace AuctionHouse.iOS
{ 
    public class MainViewController : UITableViewController
    {
        public MainViewController()
        {
            

        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        List<DataObjects.Auction> auctions;

        public async override void ViewDidLoad()
        {

            base.ViewDidLoad();

            // Perform any additional setup after loading the view
            this.NavigationItem.Title = "Auctions";

            var auctionList = await App.StoreManager.AuctionStore.GetItemsAsync();
            auctions = auctionList.ToList();
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {

            return auctions.Count;
        }

        const string AuctionCell = "AuctionCell";

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

            var itemListController = new AuctionViewController(auction.Id);

            this.NavigationController.PushViewController(itemListController, true);
        }



    }
}