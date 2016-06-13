using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using AuctionHouse.DataObjects;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionHouse.iOS
{
    public partial class AuctionListViewController : UITableViewController
    {
        public AuctionListViewController (IntPtr handle) : base (handle)
        {
        }

        List<DataObjects.Auction> auctions = new List<Auction>();

        public async Task GetData()
        {
            var auctionList = await App.StoreManager.AuctionStore.GetItemsAsync();
            auctions = auctionList.ToList();
            TableView.ReloadData();
        }

        const string AuctionCell = "AuctionCell";


        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view
            this.NavigationItem.Title = "Auctions";
            this.NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Add, async delegate
            {

                var newAuction = new Auction();
                newAuction.Location = "Melbourne";
                newAuction.AuctionDate = DateTime.Now.AddDays(1);
                await App.StoreManager.AuctionStore.InsertAsync(newAuction);
                await GetData();

            });

            await GetData();
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