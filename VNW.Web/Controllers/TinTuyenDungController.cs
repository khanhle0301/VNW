using AutoMapper;
using System.Collections.Generic;
using System.Linq;
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
            var listIdTinTuyenDung = _tinTuyenDungService.GetListBeginTin(keyword, industry, location);

            var listKyNang = _tinTuyenDungService.GetKyNang(listIdTinTuyenDung);

            var listTinh = _tinTuyenDungService.GetTinh(listIdTinTuyenDung);

            var listNganhNghe = _tinTuyenDungService.GetNganhNghe(listIdTinTuyenDung);

            var listCapBac = _tinTuyenDungService.GetCapBac(listIdTinTuyenDung);

            var tinTuyenDungHomeVm = new TinTuyenDungHomeVm();

            tinTuyenDungHomeVm.ListKyNang = listKyNang;

            tinTuyenDungHomeVm.ListTinh = listTinh;

            tinTuyenDungHomeVm.ListNganhNghe = listNganhNghe;

            tinTuyenDungHomeVm.ListCapBac = listCapBac;

            tinTuyenDungHomeVm.Keyword = keyword;

            tinTuyenDungHomeVm.Industry = industry;

            tinTuyenDungHomeVm.Location = location;

            return View(tinTuyenDungHomeVm);
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

        [HttpGet]
        public JsonResult LoadData(int page, int pageSize, string keyword = "", string industry = "", string location = "", string sort = "",
            string nganhnghe = "", string diadiem = "", string kynang = "", string capbac = "", string mucluong = "")
        {
            var model = _tinTuyenDungService.GetListSearch(keyword, industry, location, sort,
             nganhnghe, diadiem, kynang, capbac, mucluong);
            int totalRow = model.Count();
            model = model.Skip((page - 1) * pageSize).Take(pageSize);
            return Json(new
            {
                data = model,
                total = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}