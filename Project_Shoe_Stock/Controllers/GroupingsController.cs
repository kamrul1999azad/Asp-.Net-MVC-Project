using Project_Shoe_Stock.Models;
using Project_Shoe_Stock.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Project_Shoe_Stock.Controllers
{
    public class GroupingsController : Controller
    {
        private readonly ShoeDbContext db = new ShoeDbContext();
        // GET: Groupings
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GroupingByName()
        {
            var data = db.Stocks.Include(x=>x.Shoe).ToList()
            .GroupBy(s => s.Shoe.Name)
               .Select(g => new GroupData { Key = g.Key, Data = g.Select(x => x) })
               .ToList();
            return View(data);
        }
        public ActionResult GroupingBySize()
        {
            var data = db.Stocks
                .ToList()
               .GroupBy(s => s.Size)
               .Select(g => new GroupData { Key = g.Key.ToString(), Data = g.Select(x => x) })
               .ToList();
            return View(data);
        }
    }
}