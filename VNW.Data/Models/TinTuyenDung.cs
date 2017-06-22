using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VNW.Data.Models.Abstract;

namespace VNW.Data.Models
{
    [Table("TinTuyenDungs")]
    public class TinTuyenDung : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string ChucDanh { set; get; }

        [MaxLength(256)]
        public string Alias { set; get; }

        public int CapBacId { set; get; }

        public int TuLuong { set; get; }

        public int DenLuong { set; get; }

        public bool HienThiLuong { set; get; }

        public string MoTa { set; get; }

        public string YeuCau { set; get; }

        [MaxLength(256)]
        public string NguoiLienHe { set; get; }

        [MaxLength(256)]
        public string Email { set; get; }

        public int CongTyId { set; get; }

        public int? ViewCount { set; get; }

        [ForeignKey("CongTyId")]
        public virtual CongTy CongTy { set; get; }

        [ForeignKey("CapBacId")]
        public virtual CapBac CapBac { set; get; }
    }
}