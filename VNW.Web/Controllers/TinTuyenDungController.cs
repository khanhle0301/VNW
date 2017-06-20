using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VNW.Web.Controllers
{
    public class TinTuyenDungController : Controller
    {
        // GET: ViecLam
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string keyword, string industry, string location)
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }
    }
}