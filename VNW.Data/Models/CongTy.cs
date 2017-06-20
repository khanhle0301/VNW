using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VNW.Data.Models.Abstract;

namespace VNW.Data.Models
{
    [Table("CongTys")]
    public class CongTy : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string Ten { set; get; }

        [MaxLength(256)]
        public string QuyMo { set; get; }

        [MaxLength(256)]
        public string DienThoai { set; get; }

        [MaxLength(256)]
        public string DiaChi { set; get; }

        public string ThongTin { set; get; }

        [MaxLength(256)]
        public string Logo { set; get; }

        [Column(TypeName = "xml")]
        public string HinhAnh { set; get; }

        public virtual IEnumerable<TinTuyenDung> TinTuyenDungs { set; get; }
    }
}