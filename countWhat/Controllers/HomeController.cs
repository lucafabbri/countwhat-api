using countWhat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace countWhat.Controllers
{
    //[RoutePrefix("/Home")]
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

        //[Route("Counters")]
        //risponde alle richieste /Home/Counters
        public ActionResult Counters()
        {

            var counters = db.Counters.ToList();

            return View(counters); 
        }

        //[Route("Counters")]
        //risponde alle richieste /Home/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[Route("Counters")]
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
        //[Route("Edit/{id}")]
        public ActionResult Edit(string id)
        {
            var counter = db.Counters.Find(id);
            if (counter == null)
            {
                return HttpNotFound();
            }

            return View(counter);
        }

        [HttpPost]
        //[Route("Edit/{id}")]
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
        //[Route("Details/{id}")]
        public ActionResult Details(string id)
        {
            var counter = db.Counters.Find(id);
            if (counter == null)
            {
                return HttpNotFound();
            }

            return this.RedirectToAction("Counters");
        }

        //bottone remove
        //[Route("Delete/{id}")]
        public ActionResult Delete(string id)
        {
            var counter = db.Counters.Find(id);
            if (counter == null)
            {
                return HttpNotFound();
            }

            return View(counter);
        }

        [HttpDelete]
        //[Route("ConfirmDelete/{id}")]
        public ActionResult ConfirmDelete(string id)
        {
            var counter = db.Counters.Find(id);
            if (counter == null)
            {
                return HttpNotFound();
            }
            db.Counters.Remove(counter);
            db.SaveChanges();
            return this.RedirectToAction("Counters");
        }

        
        public ActionResult IncDec(string id)
        {
            var counter = db.Counters.Find(id);
            if (counter == null)
            {
                return HttpNotFound();
            }
            return View(counter);
        }

        [HttpPost]
        public ActionResult IncDec(Counter model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var counter = db.Counters.Find(model.Id);

            if (counter == null)
            {
                return View(model.Value);
            }
            
            counter.Value = model.Value;

            db.Counters.Add(counter);
            db.Entry(counter).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();

            return this.RedirectToAction("Counters");
        }
    }
}