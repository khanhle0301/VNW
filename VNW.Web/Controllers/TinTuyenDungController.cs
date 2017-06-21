using System.Web.Mvc;
using VNW.Service;

namespace VNW.Web.Controllers
{
    public class TinTuyenDungController : Controller
    {
        private ITinTuyenDungService _tinTuyenDungService;

        public TinTuyenDungController(ITinTuyenDungService tinTuyenDungService)
        {
            this._tinTuyenDungService = tinTuyenDungService;
        }

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

        [HttpGet]
        public JsonResult GetListTinByName(string keyword)
        {
            var model = _tinTuyenDungService.GetListTinByName(keyword);
            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }
    }
}