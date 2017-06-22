using System.Collections.Generic;
using VNW.Data.ViewModel;

namespace VNW.Web.Models
{
    public class TinTuyenDungHomeVm
    {
        public IEnumerable<KyNangVm> ListKyNang;

        public IEnumerable<TinhVm> ListTinh;

        public IEnumerable<NganhNgheVm> ListNganhNghe;

        public IEnumerable<CapBacVm> ListCapBac;

        public string Keyword;

        public string Industry;

        public string Location;
    }
}