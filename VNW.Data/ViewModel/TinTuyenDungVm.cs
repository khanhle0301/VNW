using System;

namespace VNW.Data.ViewModel
{
    public class TinTuyenDungVm
    {
        public int Id { set; get; }

        public string ChucDanh { set; get; }

        public int TuLuong { set; get; }

        public int DenLuong { set; get; }

        public bool HienThiLuong { set; get; }

        public string TenCongTy { set; get; }

        public string Logo { set; get; }

        public DateTime? NgayDang { set; get; }
    }
}