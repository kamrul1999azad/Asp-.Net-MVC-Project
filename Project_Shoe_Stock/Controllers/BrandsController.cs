using Project_Shoe_Stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace Project_Shoe_Stock.Controllers
{
    public class BrandsController : Controller
    {
        protected readonly ShoeDbContext db = new ShoeDbContext();
       
        // GET: Brands
        public ActionResult Index()
        {
            var data = db.Brands.ToList();
            db.Shoes.Add(new Shoe());
            return View(data);
        }

        public ActionResult Create()
        {
           return View();   
        }
        [HttpPost]
        public ActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {

                db.Brands.Add(brand);
                db.SaveChanges();
                return PartialView("_Success");
            }

            return PartialView("_Fail"); 



        }
        public ActionResult Edit(int id)
        {
            var data = db.Brands.FirstOrDefault(x => x.BrandId == id);
            if (data == null) return new HttpNotFoundResult();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Brand model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");

        }
        [HttpPost]

        public ActionResult Delete(int id)
        {
            var brand = db.Brands.FirstOrDefault(x => x.BrandId == id);
            if (brand == null) return new HttpNotFoundResult();

            db.Brands.Remove(brand);
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}