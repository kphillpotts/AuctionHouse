using FFImageLoading;
using Foundation;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UIKit;

namespace AuctionHouse.iOS
{
    public partial class AuctionItemCell : UITableViewCell
    {
        public AuctionItemCell(IntPtr handle) : base(handle)
        {
        }

        public async Task Update(AuctionHouse.DataObjects.AuctionItem item)
        {
            TextLabel.Text = item.Description;
            DetailTextLabel.Text = item.Details;
            Accessory = UITableViewCellAccessory.DisclosureIndicator;

            //ImageView.Image = await LoadImageFromURL(item.Image);

            Console.WriteLine("Requesting image " + item.Image);

            await ImageService.Instance.LoadUrl(item.Image)
                .Success((size, loadingResult) =>
                   {
                       Console.WriteLine("Loading result " + loadingResult.ToString());
                   })
                .Error((ex) =>
                   {
                       Console.WriteLine("Error");
                   })
                .Finish(worksheduled =>
                    {
                        Console.WriteLine("Completed : " + worksheduled.IsCancelled + ", " + worksheduled.Completed);
                    })
                .IntoAsync(ImageView);
        }

        public static async Task<UIImage> LoadImageFromURL(string imageUrl)
        {
            var httpClient = new HttpClient();

            Task<byte[]> contentsTask = httpClient.GetByteArrayAsync(imageUrl);

            // await! control returns to the caller and the task continues to run on another thread
            var contents = await contentsTask;

            // load from bytes
            return UIImage.LoadFromData(NSData.FromArray(contents));
        }
    }
}