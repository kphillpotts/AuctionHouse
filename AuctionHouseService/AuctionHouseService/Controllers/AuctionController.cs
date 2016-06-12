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
    public class AuctionController : TableController<Auction>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Auction>(context, Request);
        }

        // GET tables/Auction
        public IQueryable<Auction> GetAllAuction()
        {
            return Query(); 
        }

        // GET tables/Auction/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Auction> GetAuction(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Auction/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Auction> PatchAuction(string id, Delta<Auction> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Auction
        public async Task<IHttpActionResult> PostAuction(Auction item)
        {
            Auction current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Auction/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAuction(string id)
        {
             return DeleteAsync(id);
        }
    }
}
