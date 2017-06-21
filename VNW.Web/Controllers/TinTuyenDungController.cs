using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using VNW.Data.Models;
using VNW.Service;
using VNW.Web.Models;

namespace VNW.Web.Controllers
{
    public class TinTuyenDungController : Controller
    {
        private ITinTuyenDungService _tinTuyenDungService;
        private INganhNgheService _nganhNgheService;
        private ITinhService _tinhService;

        public TinTuyenDungController(ITinTuyenDungService tinTuyenDungService, INganhNgheService nganhNgheService,
           ITinhService tinhService)
        {
            this._tinTuyenDungService = tinTuyenDungService;
            this._nganhNgheService = nganhNgheService;
            this._tinhService = tinhService;
        }

        public ActionResult Search(string keyword, string industry, string location)
        {
            var s = _tinTuyenDungService.GetListBeginTin(keyword, industry, location);
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

        [ChildActionOnly]
        public PartialViewResult Category()
        {
            var model = _nganhNgheService.GetAllByChild();
            var viewModel = Mapper.Map<IEnumerable<NganhNghe>, IEnumerable<NganhNgheViewModel>>(model);
            return PartialView(viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult Location()
        {
            var model = _tinhService.GetAll();
            var viewModel = Mapper.Map<IEnumerable<Tinh>, IEnumerable<TinhViewModel>>(model);
            return PartialView(viewModel);
        }
    }
}