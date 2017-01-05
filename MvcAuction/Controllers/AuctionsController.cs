using MvcAuction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcAuction.Controllers
{
    public class AuctionsController : Controller
    {
        //
        // GET: /Auctions/

        [AllowAnonymous]
        [OutputCache(Duration = 1)]
        public ActionResult Index()
        {  
            //var auctions = new[] {
            //    new Models.Auction()
            //    {
            //        Title = "Example Auction #1",
            //        Description = "This is an example Auction",
            //        StartTime = DateTime.Now,
            //        EndTime = DateTime.Now.AddDays(7),
            //        StartPrice = 1.00m,
            //        CurrentPrice = null,
            //    },
            //    new Models.Auction()
            //    {
            //        Title = "Example Auction #2",
            //        Description = "This is a second Auction",
            //        StartTime = DateTime.Now,
            //        EndTime = DateTime.Now.AddDays(7),
            //        StartPrice = 1.00m,
            //        CurrentPrice = 30m,
            //    },
            //    new Models.Auction()
            //    {
            //        Title = "Example Auction #3",
            //        Description = "This is a third Auction",
            //        StartTime = DateTime.Now,
            //        EndTime = DateTime.Now.AddDays(7),
            //        StartPrice = 10.00m,
            //        CurrentPrice = 24m,
            //    },
            //};

            var db = new AuctionsDataContext();
            // this tells entity framework to go and retrieve the data from DB. 
            //go and execute the sql code, 
            var auctions = db.Auctions.ToArray();

            return View(auctions);
        }

        [OutputCache(Duration = 10)]
        public ActionResult Auction(long id)
        {   
            //var auction = new MvcAuction.Models.Auction()
            //{
            //    Title = "Example Auction",
            //    Description = "This is an example Auction",
            //    StartTime = DateTime.Now,
            //    EndTime = DateTime.Now.AddDays(7),
            //    StartPrice = 1.00m,
            //    CurrentPrice = null,
            //};
            var db = new AuctionsDataContext();
            var auction = db.Auctions.Find(id);

            return View(auction);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bid(Bid bid)
        {
            var db = new AuctionsDataContext();
            var auction = db.Auctions.Find(bid.AuctionId);

            if (auction == null)
            {
                ModelState.AddModelError("AuctionId", "Auction not found!");
            }
            else if (auction.CurrentPrice >= bid.Amount)
            {
                ModelState.AddModelError("Amount", "Bid amount must exceed current bid");
            }
            else
            {
                bid.Username = User.Identity.Name;
                auction.Bids.Add(bid);
                auction.CurrentPrice = bid.Amount;
                db.SaveChanges();
            }
            //all other requsts should still be redirected in response to the auction action
            if (!Request.IsAjaxRequest())
                return RedirectToAction("Auction", new { id = bid.AuctionId });
            // this does not return any html so we are replacing with a partial view
            //var httpStatus = ModelState.IsValid ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            //return new HttpStatusCodeResult(httpStatus);
            //partial views are only evenr mean to be sent in response an ajax requests
            //return PartialView("_CurrentPrice", auction);
            return Json(new {
                CurrentPrice = bid.Amount.ToString("C"),
                BidCount = auction.BidCount
            });
        }


        [HttpGet]
        public ActionResult Create()
        {
            var categoryList = new SelectList(new[] { "Automotive", "Electronics", "Games", "Home" });
            ViewBag.CategoryList = categoryList;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create([Bind(Exclude="CurrentPrice")]Models.Auction auction)
        {
            if (ModelState.IsValid)
            {
                // Save to the database
                var db = new AuctionsDataContext();
                db.Auctions.Add(auction);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
                
            return Create();
        }
    }
}
