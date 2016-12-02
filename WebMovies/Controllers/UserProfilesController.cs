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
    public class UserProfilesController : Controller
    {
        private MyWebMoviesEntities db = new MyWebMoviesEntities();

        // GET: UserProfiles
        public async Task<ActionResult> Index()
        {
            var userProfiles = db.UserProfiles.Include(u => u.Country1).Include(u => u.Language1);
            return View(await userProfiles.ToListAsync());
        }

        // GET: UserProfiles/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = await db.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // GET: UserProfiles/Create
        public ActionResult Create()
        {
            ViewBag.country = new SelectList(db.Countries, "countryCode", "countryName");
            ViewBag.language = new SelectList(db.Languages, "languageCode", "languageName");
            return View();
        }

        // POST: UserProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "usrId,usrlogin,enPassword,firstName,lastName,email,language,country")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userProfile);
                await db.SaveChangesAsync();
                return RedirectToAction("Create");
            }

            ViewBag.country = new SelectList(db.Countries, "countryCode", "countryName", userProfile.country);
            ViewBag.language = new SelectList(db.Languages, "languageCode", "languageName", userProfile.language);
            return View(userProfile);
        }

        // GET: UserProfiles/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = await db.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.country = new SelectList(db.Countries, "countryCode", "countryName", userProfile.country);
            ViewBag.language = new SelectList(db.Languages, "languageCode", "languageName", userProfile.language);
            return View(userProfile);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "usrId,usrlogin,enPassword,firstName,lastName,email,language,country")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userProfile).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Edit");
            }
            ViewBag.country = new SelectList(db.Countries, "countryCode", "countryName", userProfile.country);
            ViewBag.language = new SelectList(db.Languages, "languageCode", "languageName", userProfile.language);
            return View(userProfile);
        }

        // GET: UserProfiles/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = await db.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            UserProfile userProfile = await db.UserProfiles.FindAsync(id);
            db.UserProfiles.Remove(userProfile);
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
