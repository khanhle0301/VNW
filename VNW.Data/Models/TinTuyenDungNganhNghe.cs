using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VNW.Data.Models
{
    [Table("TinTuyenDungNganhNghes")]
    public class TinTuyenDungNganhNghe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public int TinTuyenDungId { set; get; }

        public int NganhNgheId { set; get; }

        [ForeignKey("TinTuyenDungId")]
        public virtual TinTuyenDung TinTuyenDung { set; get; }

        [ForeignKey("NganhNgheId")]
        public virtual NganhNghe NganhNghe { set; get; }
    }
}