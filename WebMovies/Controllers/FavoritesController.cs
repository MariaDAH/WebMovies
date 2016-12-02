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
    public class FavoritesController : Controller
    {
        private MyWebMoviesEntities db = new MyWebMoviesEntities();

        // GET: Favorites
        public async Task<ActionResult> Index()
        {
            var favorites = db.Favorites.Include(f => f.Link).Include(f => f.UserProfile);
            return View(await favorites.ToListAsync());
        }

        // GET: Favorites/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = await db.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // GET: Favorites/Create
        public ActionResult Create()
        {
            ViewBag.linkId = new SelectList(db.Links, "linkId", "name");
            ViewBag.usrId = new SelectList(db.UserProfiles, "usrId", "usrlogin");
            return View();
        }

        // POST: Favorites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "favoriteId,usrId,linkId,name,description,date")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                db.Favorites.Add(favorite);
                await db.SaveChangesAsync();
                return RedirectToAction("Create");
            }

            ViewBag.linkId = new SelectList(db.Links, "linkId", "name", favorite.linkId);
            ViewBag.usrId = new SelectList(db.UserProfiles, "usrId", "usrlogin", favorite.usrId);
            return View(favorite);
        }

        // GET: Favorites/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = await db.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            ViewBag.linkId = new SelectList(db.Links, "linkId", "name", favorite.linkId);
            ViewBag.usrId = new SelectList(db.UserProfiles, "usrId", "usrlogin", favorite.usrId);
            return View(favorite);
        }

        // POST: Favorites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "favoriteId,usrId,linkId,name,description,date")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favorite).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Edit");
            }
            ViewBag.linkId = new SelectList(db.Links, "linkId", "name", favorite.linkId);
            ViewBag.usrId = new SelectList(db.UserProfiles, "usrId", "usrlogin", favorite.usrId);
            return View(favorite);
        }

        // GET: Favorites/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = await db.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // POST: Favorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Favorite favorite = await db.Favorites.FindAsync(id);
            db.Favorites.Remove(favorite);
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
