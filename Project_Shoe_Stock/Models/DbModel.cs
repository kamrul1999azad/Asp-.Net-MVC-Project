using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_Shoe_Stock.Models
{
    public enum Size { A38=38, A39,A40, A41, A42, A43, A44, U7=7, U8, U9, U10}
    public class ShoeModel
    {
        public int ShoeModelId {  get; set; }
        [Required, StringLength(50), Display(Name ="Model name")]
        public string ModelName { get; set; }
        public virtual ICollection<Shoe> Shoes { get; set; }= new List<Shoe>();
    }
    public class Brand
    {
        public int BrandId { get; set; }
        [Required, StringLength(50), Display(Name = "Brand name")]
        public string BrandName { get; set; }
        public virtual ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
    }
    public class Shoe
    {
        public int ShoeId { get; set; }
        [Required, StringLength(40)]
        public string Name { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "First introduced"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FirstIntroducedOn { get; set; }
        public bool OnSale { get; set; }
        [Required, StringLength(50)]
        public string Picture {  get; set; }
        [Required, ForeignKey("ShoeModel")]
        public int ShoeModelId { get; set; }
        [Required, ForeignKey("Brand")]
        public int BrandId { get; set; }
        public virtual ShoeModel ShoeModel { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }=new List<Stock>();
    }
    public class Stock
    {
        public int StockId { get; set; }
        //[EnumDataType(typeof(Stock))]
        public Size Size { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [Required, ForeignKey("Shoe")]
        public int ShoeId { get; set; }
        public virtual Shoe Shoe { get; set; }
    }
    public class ShoeDbContext : DbContext
    {
        public ShoeDbContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }
        
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ShoeModel> ShoeModels { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Stock> Stocks { get; set; }
    }
}