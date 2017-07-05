using countWhat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace countWhat.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }

        // risponde alle richieste /Home/Index ovver /Home ovvero /
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        //risponde alle richieste /Home/Counters
        public ActionResult Counters()
        {

            var counters = db.Counters.ToList();

            return View(counters); 
        }

        //risponde alle richieste /Home/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Counter model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Id = Guid.NewGuid().ToString();
            db.Counters.Add(model);
            db.SaveChanges();

            return this.RedirectToAction("Counters");
        }

        //bottone edit
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Counter model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var counter = db.Counters.Find(model.Id);

            if (counter == null)
            {
                return View(model);
            }

            counter.What = model.What;
            counter.Value = model.Value;

            db.Counters.Add(counter);
            db.Entry(counter).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();

            return this.RedirectToAction("Counters");
        }

        //bottone details
        public ActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(Counter model)
        {
            return View(model);
        }

        //bottone remove
        public ActionResult Delete()
        {
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(Counter model)
        {
            db.Counters.Remove(model);
            db.SaveChanges();
            
            return this.RedirectToAction("Counters");
        }
    }
}