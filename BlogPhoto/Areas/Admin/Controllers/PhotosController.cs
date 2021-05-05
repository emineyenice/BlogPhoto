using BlogPhoto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPhoto.Areas.Admin.Controllers
{
    public class PhotosController : AdminBaseController
    {
        // GET: Admin/Photos
        public ActionResult Index()
        {
            return View(db.Photos.ToList());
        }

        public ActionResult Add()
        {
            ViewBag.HashTag = new SelectList(db.HashTags, "Id", "HashTagName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Photo photo, HttpPostedFileBase image, int[] Id)
        {
            //https://www.mikesdotnetting.com/article/265/asp-net-mvc-dropdownlists-multiple-selection-and-enum-support
            ResimHataKontrolleri(image);
            if (ModelState.IsValid)
            {
                photo.ImagePath = ResimYukle(image);
                db.Photos.Add(photo);
                if (Id != null)
                {
                    for (int i = 0; i < Id.Length; i++)
                    {
                        HashTag hashTag = db.HashTags.Find(Id[i]);
                        photo.HashTags.Add(hashTag);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HashTag = new SelectList(db.HashTags, "Id", "HashTagName");
            return View();
        }

        public ActionResult Edit(int id)
        {
            var photo = db.Photos.Find(id);
            if (photo == null) return HttpNotFound();
            ViewBag.HashTagId = new SelectList(db.HashTags, "Id", "HashTagName");
            return View(photo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Photo photo, HttpPostedFileBase image, bool resmiKaldir)
        {
            ResimHataKontrolleri(image);
            if (ModelState.IsValid)
            {
                var resimYolu = ResimYukle(image);
                if (resimYolu != null || resmiKaldir)
                {
                    ResimSil(photo.ImagePath);  
                    photo.ImagePath = resimYolu;
                }
                db.Entry(photo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HashTagId = new SelectList(db.HashTags, "Id", "HashTagName");
            return View(photo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var image = db.Photos.Find(id);
            if (image == null) return HttpNotFound();

            db.Photos.Remove(image);
            db.SaveChanges();
            ResimSil(image.ImagePath);
            TempData["mesaj"] = $"\"{image.PhotoTitle}\" adlı ürün başarıyla silindi."; //TempData sayfalar arası 1 kerelik veri taşır.
            return RedirectToAction("Index");

        }

        private void ResimSil(string resimYolu)
        {
            if (!string.IsNullOrEmpty(resimYolu))
            {
                var silYolu = Path.Combine(Server.MapPath("~/Images/Uploads"), resimYolu);

                if (System.IO.File.Exists(silYolu))
                {
                    System.IO.File.Delete(silYolu);
                }
            }
        }
        private void ResimHataKontrolleri(HttpPostedFileBase resim)
        {
            string[] izinVerilenler = { ".jpg", ".jpeg", ".png" };
            if (resim != null)
            {
                if (!izinVerilenler.Contains(Path.GetExtension(resim.FileName).ToLower()))
                {
                    ModelState.AddModelError("resim", "İzin verilen dosya uzantıları: jpg/jpeg, png");
                }

                else if (resim.ContentLength > 1000 * 1000)
                {
                    ModelState.AddModelError("resim", "Maksimum resim boyutu:1mb olmalıdır.");
                }
            }
        }

        private string ResimYukle(HttpPostedFileBase resim)
        {
            if (resim == null)
                return null;

            var dosyaAd = Guid.NewGuid().ToString() + Path.GetExtension(resim.FileName);
            var yuklemeKlasoruYolu = Server.MapPath("~/Images/Uploads");
            var kaydetYol = Path.Combine(yuklemeKlasoruYolu, dosyaAd);
            resim.SaveAs(kaydetYol);
            return dosyaAd;
        }
    }
}