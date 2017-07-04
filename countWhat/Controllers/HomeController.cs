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
        private object counters;

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
            return View(counters);
        }

        [HttpGet]
        public ActionResult Edit(Counter model_edit)
        {   
            db.Counters.
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

            return this.RedirectToAction("Counters");
        }

        //bottone remove
        public ActionResult Delete()
        {
            return View(counters);
        }

        [HttpGet]
        public ActionResult Delete(Counter model)
        {
            db.Counters.Remove(model);
            db.SaveChanges();

            return this.RedirectToAction("Counters");
        }
    }
}