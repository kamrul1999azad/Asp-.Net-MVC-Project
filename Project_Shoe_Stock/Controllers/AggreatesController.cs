using Project_Shoe_Stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Project_Shoe_Stock.Controllers
{
    public class AggreatesController : Controller
    {
        private readonly ShoeDbContext db=new ShoeDbContext();
        // GET: Aggreates
        public ActionResult Index()
        {
            var data = db.Stocks.ToList();
            return View(data);
        }
    }
}