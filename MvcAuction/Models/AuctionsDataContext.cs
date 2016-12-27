using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcAuction.Models
{
    public class AuctionsDataContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }

        static AuctionsDataContext()
        {
            //Entity database intialiser changes the db if db model changes
            System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AuctionsDataContext>());
        }

    }
}