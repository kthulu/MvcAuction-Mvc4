using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcAuction.Models
{
    public class Auction
    {
      
        [Required]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength:200,MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Category { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name="Image Url")]
        public string ImageUrl { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name="Start Time")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name ="End Time")]
        public DateTime EndTime { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name="Starting Price")]
        public decimal StartPrice { get; set; }

        
        [Display(Name="Current Price")]
        [DataType(DataType.Currency)]
        public decimal? CurrentPrice { get; set; }
        //set to virtual so that entity framework can overight it when it retrieves is from the database
        public virtual Collection<Bid> Bids { get; private set; } // only this class will set the property


        public int BidCount => Bids.Count;
        //{
        //    get { return Bids.Count; }
        //}

        public Auction()
        {
            Bids = new Collection<Bid>();
        }
    }    
}