using BlogPhoto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPhoto.Controllers
{
    public class HomeController : Controller
    {
        PhotoContext db = new PhotoContext();
        public ActionResult Index()
        {
            return View(db.Photos.OrderByDescending(x => x.Id).ToList());
        }

       
    }
}