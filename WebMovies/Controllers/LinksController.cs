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
    public class LinksController : Controller
    {
        private MyWebMoviesEntities db = new MyWebMoviesEntities();

        // GET: Links
        public async Task<ActionResult> Index()
        {
            var links = db.Links.Include(l => l.UserProfile);
            return View(await links.ToListAsync());
        }

        // GET: Links/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = await db.Links.FindAsync(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // GET: Links/Create
        public ActionResult Create()
        {
            ViewBag.usrId = new SelectList(db.UserProfiles, "usrId", "usrlogin");
            return View();
        }

        // POST: Links/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "linkId,usrId,movieId,name,url,description,date,reportRead")] Link link)
        {
            if (ModelState.IsValid)
            {
                db.Links.Add(link);
                await db.SaveChangesAsync();
                return RedirectToAction("Create");
            }

            ViewBag.usrId = new SelectList(db.UserProfiles, "usrId", "usrlogin", link.usrId);
            return View(link);
        }

        // GET: Links/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = await db.Links.FindAsync(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            ViewBag.usrId = new SelectList(db.UserProfiles, "usrId", "usrlogin", link.usrId);
            return View(link);
        }

        // POST: Links/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "linkId,usrId,movieId,name,url,description,date,reportRead")] Link link)
        {
            if (ModelState.IsValid)
            {
                db.Entry(link).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Edit");
            }
            ViewBag.usrId = new SelectList(db.UserProfiles, "usrId", "usrlogin", link.usrId);
            return View(link);
        }

        // GET: Links/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = await db.Links.FindAsync(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // POST: Links/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Link link = await db.Links.FindAsync(id);
            db.Links.Remove(link);
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
