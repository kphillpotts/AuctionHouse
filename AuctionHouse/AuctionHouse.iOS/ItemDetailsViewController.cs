using AuctionHouse.DataObjects;
using Foundation;
using System;
using UIKit;

namespace AuctionHouse.iOS
{
    public partial class ItemDetailsViewController : UIViewController
    {
        public ItemDetailsViewController (IntPtr handle) : base (handle)
        {
        }

        public AuctionItem AuctionItem { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.Title = AuctionItem.Description;
            this.NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Bookmarks, null);

            LotLabel.Text = AuctionItem.LotNumber.ToString();
            NameLabel.Text = AuctionItem.Description.ToString();
            DescriptionLabel.Text = AuctionItem.Details.ToString();
            EstimateLabel.Text = AuctionItem.LowerEstimate.ToString("C") + " - " + AuctionItem.UpperEstimate.ToString("C");
        }


    }
}