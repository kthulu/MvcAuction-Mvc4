using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcAuction.Models
{
    public class Auction
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }

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
    }    
}