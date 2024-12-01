using Project_Shoe_Stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using X.PagedList;

namespace Project_Shoe_Stock.Controllers
{
    public class ShoeModelsController : Controller
    {
        private readonly ShoeDbContext db = new ShoeDbContext();
        // GET: ShoeModels
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ShoeModel model)
        {
            if(ModelState.IsValid)
            {
                db.ShoeModels.Add(model);
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Edit(int id)
        {
            var data = db.ShoeModels.FirstOrDefault(x=> x.ShoeModelId == id);
            if(data == null) return new HttpNotFoundResult();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(ShoeModel model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public PartialViewResult ModelsTable(int pg=1)
        {
            var data = db.ShoeModels.OrderBy(x => x.ShoeModelId).ToPagedList(pg, 5);
            return PartialView("_ShoeModels", data);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var data=db.ShoeModels.FirstOrDefault(x=>x.ShoeModelId==id);
            if (data == null) return new HttpNotFoundResult();
            db.ShoeModels.Remove(data);
            db.SaveChanges();
            return Json(new {success=true});
        }
    }
}