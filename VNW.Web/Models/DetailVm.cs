using System.Collections.Generic;
using VNW.Data.Models;
using VNW.Data.ViewModel;

namespace VNW.Web.Models
{
    public class DetailVm
    {
        public TinTuyenDung TinTuyenDung;

        public IEnumerable<KyNangVm> KyNang;

        public IEnumerable<PhucLoiVm> PhucLoi;

        public IEnumerable<NganhNgheVm> NganhNghe;

        public IEnumerable<TinhVm> Tinh;

        public List<string> ListImages;
    }
}