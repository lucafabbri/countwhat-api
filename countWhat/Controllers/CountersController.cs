using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using countWhat.Models;
using Microsoft.AspNet.Identity;

namespace countWhat.Controllers
{
    [Authorize]
    public class CountersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Counters
        public IEnumerable<Counter> GetCounters()
        {
            string userid = User.Identity.GetUserId();
            return db.Counters.Where(c=>c.OwnerId==userid).ToList();
        }

        // GET: api/Counters/5
        [ResponseType(typeof(Counter))]
        public IHttpActionResult GetCounter(string id)
        {
            Counter counter = db.Counters.Find(id);
            if (counter == null)
            {
                return NotFound();
            }

            return Ok(counter);
        }

        // PUT: api/Counters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCounter(string id, Counter counter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != counter.Id)
            {
                return BadRequest();
            }

            db.Entry(counter).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CounterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Counters
        [ResponseType(typeof(Counter))]
        public IHttpActionResult PostCounter(Counter counter)
        {
            counter.Id = Guid.NewGuid().ToString();
            counter.OwnerId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Counters.Add(counter);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CounterExists(counter.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = counter.Id }, counter);
        }

        // DELETE: api/Counters/5
        [ResponseType(typeof(Counter))]
        public IHttpActionResult DeleteCounter(string id)
        {
            Counter counter = db.Counters.Find(id);
            if (counter == null)
            {
                return NotFound();
            }

            db.Counters.Remove(counter);
            db.SaveChanges();

            return Ok(counter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CounterExists(string id)
        {
            return db.Counters.Count(e => e.Id == id) > 0;
        }
    }
}