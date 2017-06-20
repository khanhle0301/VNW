using System.Linq;
using System.Web.Mvc;
using TeduShop.Service;

namespace VNW.Web.Controllers
{
    public class HomeController : Controller
    {
        private INganhNgheService _nganhNgheService;

        public HomeController(INganhNgheService nganhNgheService)
        {
            this._nganhNgheService = nganhNgheService;
        }

        public ActionResult Index()
        {
            var s = _nganhNgheService.GetAll().ToList();
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult Footer()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult Menu()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult Notification()
        {
            return PartialView();
        }
    }
}