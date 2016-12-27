using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAuction.Models;

namespace MvcAuction.Controllers
{
    public class AuctionsController : Controller
    {
        //
        // GET: /Auctions/

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

        [HttpGet]
        public ActionResult Create()
        {
            var categories = new SelectList(new[] { "Option1", "Option2", "Option3" });
            
            ViewBag.CategoryList = categories;

            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "CurrentPrice")]Models.Auction auction)
        {
            //if (string.IsNullOrWhiteSpace(auction.Title))
            //{
            //    ModelState.AddModelError("Title", "Title is invalid");
            //}
            //else if (auction.Title.Length > 5 || auction.Title.Length > 200)
            //{
            //    ModelState.AddModelError("Title", "Title must be between 5 and 200 charactera long");
            //}

            if (ModelState.IsValid)
            {
                var db = new AuctionsDataContext();
                db.Auctions.Add(auction);
                db.SaveChanges();

                RedirectToAction("Index");
            }
                return Create();
            
        }
    }
}
