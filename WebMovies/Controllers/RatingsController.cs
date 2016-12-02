using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebMovies.Models;

namespace WebMovies.Controllers
{
    public class RatingsController : Controller
    {
        private MyWebMoviesEntities db = new MyWebMoviesEntities();

        // GET: Ratings
        public async Task<ActionResult> Index()
        {
            var ratings = db.Ratings.Include(r => r.Link).Include(r => r.UserProfile);
            return View(await ratings.ToListAsync());
        }

        // GET: Ratings/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = await db.Ratings.FindAsync(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: Ratings/Create
        public ActionResult Create()
        {
            ViewBag.linkId = new SelectList(db.Links, "linkId", "name");
            ViewBag.usrId = new SelectList(db.UserProfiles, "usrId", "usrlogin");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ratingId,usrId,linkId,value,date")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                await db.SaveChangesAsync();
                return RedirectToAction("Create");
            }

            ViewBag.linkId = new SelectList(db.Links, "linkId", "name", rating.linkId);
            ViewBag.usrId = new SelectList(db.UserProfiles, "usrId", "usrlogin", rating.usrId);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = await db.Ratings.FindAsync(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.linkId = new SelectList(db.Links, "linkId", "name", rating.linkId);
            ViewBag.usrId = new SelectList(db.UserProfiles, "usrId", "usrlogin", rating.usrId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ratingId,usrId,linkId,value,date")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Edit");
            }
            ViewBag.linkId = new SelectList(db.Links, "linkId", "name", rating.linkId);
            ViewBag.usrId = new SelectList(db.UserProfiles, "usrId", "usrlogin", rating.usrId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = await db.Ratings.FindAsync(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Rating rating = await db.Ratings.FindAsync(id);
            db.Ratings.Remove(rating);
            await db.SaveChangesAsync();
            return RedirectToAction("Delete");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
