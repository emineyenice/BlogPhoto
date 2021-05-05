using BlogPhoto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPhoto.Areas.Admin.Controllers
{
    public class HashTagsController : AdminBaseController
    {
        // GET: Admin/HashTags
        public ActionResult Index()
        {
            return View(db.HashTags.ToList());
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(HashTag hashTag)
        {
            if (ModelState.IsValid)
            {
                db.HashTags.Add(hashTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


        public ActionResult Edit(int id)
        {
            var hashTag = db.HashTags.Find(id);
            if (hashTag == null) return HttpNotFound();
            return View(hashTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HashTag hashTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hashTag).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var hashTag = db.HashTags.Find(id);
            if (hashTag == null)
            {
                return HttpNotFound();
            }
            db.HashTags.Remove(hashTag);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}