using Project_Shoe_Stock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Shoe_Stock.ViewModels
{
    public class ShoeInputModel
    {
        public int ShoeId { get; set; }
        [Required, StringLength(40)]
        public string Name { get; set; }
        [Required,DataType(DataType.Date), Display(Name = "First introduced"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FirstIntroducedOn { get; set; }
        public bool OnSale { get; set; }
        [Required]
        public HttpPostedFileBase Picture { get; set; }
        [Required]
        public int ShoeModelId { get; set; }
        [Required]
        public int BrandId { get; set; }
        
        public  List<Stock> Stocks { get; set; } = new List<Stock>();
    }
}