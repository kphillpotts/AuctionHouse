using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using AuctionHouseService.DataObjects;
using AuctionHouseService.Models;

namespace AuctionHouseService.Controllers
{
    public class AuctionItemController : TableController<AuctionItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<AuctionItem>(context, Request);
        }

        // GET tables/AuctionItem
        public IQueryable<AuctionItem> GetAllAuctionItem()
        {
            return Query(); 
        }

        // GET tables/AuctionItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<AuctionItem> GetAuctionItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/AuctionItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<AuctionItem> PatchAuctionItem(string id, Delta<AuctionItem> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/AuctionItem
        public async Task<IHttpActionResult> PostAuctionItem(AuctionItem item)
        {
            AuctionItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/AuctionItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAuctionItem(string id)
        {
             return DeleteAsync(id);
        }
    }
}
