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
using PagedList;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Simple.ImageResizer.MvcExtensions;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;
using DelegateDecompiler;

namespace WebMovies.Controllers
{
    public class FavoritesController : Controller
    {
        #region Properties

        private MyWebMoviesEntities db = new MyWebMoviesEntities();

        private class ImageToDisplay 
        {
            public string ImageURL;
            public string FavoriteName;
        }

        #endregion 

        #region CRUD_EF_Methods

        // GET: Favorites
        //public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchData, int? pageNo)


        //public ActionResult Index(string sortOrder, string currentFilter, string searchData, int? pageNo)
        //{

        public async Task<ActionResult> Index(string sortOrder, string searchData)
        {
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.SortingName = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.SortingDate = "Date";

            //if (searchData != null)
            //{
            //    pageNo = 1;
            //}
            //else
            //{
            //    searchData = currentFilter;
            //}

            ViewBag.CurrentFilter = searchData;

            var favorites = db.Favorites.Include(f => f.Link).Include(f => f.UserProfile);

            if (!String.IsNullOrEmpty(searchData))
            {
                favorites = favorites.Where(fav => fav.name.ToUpper().Contains(searchData.ToUpper())
                    || fav.Link.name.ToUpper().Contains(searchData.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Name":
                    favorites = favorites.OrderByDescending(fav => fav.name);
                    break;
                case "Date":
                    favorites = favorites.OrderByDescending(fav => fav.date);
                    break;
                default:
                    favorites = favorites.OrderByDescending(fav => fav.name);
                    break;
            }


            //int pageSize = 4;
            //int pageNumber = (pageNo ?? 1);


            //return View(await favorites.ToPagedListAsync(No_Of_Page, Size_Of_Page));
            //return View(favorites.ToPagedList(pageNumber, pageSize));
            return View(await favorites.ToListAsync());
        }


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

        //// GET: Favorites/Create
        [Computed]
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
        
        #endregion 

        #region ViewDinamycMethods

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Autocomplete(string prefix)
        {
            var favoriteName = db.Favorites.Where(s => s.name.ToLower().Contains
                          (prefix.ToLower())).Select(w => w).ToList();
            return Json(favoriteName, JsonRequestBehavior.AllowGet);
        } //End Action Autocomplete

        public JsonResult GetImageList()
        {
            List<ImageToDisplay> ls = new List<ImageToDisplay>();

            ls.Add(new ImageToDisplay { ImageURL = Url.Content("~/Content/images/darkcrystal1982.jpg"), FavoriteName = "DarkCryst" });
            ls.Add(new ImageToDisplay { ImageURL = Url.Content("~/Content/images/labyrith1986.jpg"), FavoriteName = "Labyr" });
            ls.Add(new ImageToDisplay { ImageURL = Url.Content("~/Content/images/romanholidays1953.jpg"), FavoriteName = "RomanHol" });
            ls.Add(new ImageToDisplay { ImageURL = Url.Content("~/Content/images/seven1995.jpg"), FavoriteName = "Seven" });
            ls.Add(new ImageToDisplay { ImageURL = Url.Content("~/Content/images/tesis1996.jpg"), FavoriteName = "Tesis" });
            ls.Add(new ImageToDisplay { ImageURL = Url.Content("~/Content/images/the4feathers1977.jpg"), FavoriteName = "the4feath" });
            ls.Add(new ImageToDisplay { ImageURL = Url.Content("~/Content/images/theneverendingstory1984.jpg"), FavoriteName = "the4feath" });
            ls.Add(new ImageToDisplay { ImageURL = Url.Content("~/Content/images/thepincessbride1987.jpg"), FavoriteName = "theprincess" });
            ls.Add(new ImageToDisplay { ImageURL = Url.Content("~/Content/images/willow1988.jpg"), FavoriteName = "will" });

            return Json(ls, JsonRequestBehavior.AllowGet);
        } // End Action GetImageList

        #endregion
    }
}
