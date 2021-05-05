using BlogPhoto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPhoto.Areas.Admin.Controllers
{
    public abstract class AdminBaseController : Controller
    {
        protected PhotoContext db = new PhotoContext();
        // GET: Admin/AdminBase
       
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