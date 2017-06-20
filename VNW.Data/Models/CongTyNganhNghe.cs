using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VNW.Data.Models
{
    [Table("CongTyNganhNghes")]
    public class CongTyNganhNghe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public int CongTyId { set; get; }

        public int NganhNgheId { set; get; }

        [ForeignKey("CongTyId")]
        public virtual CongTy CongTy { set; get; }

        [ForeignKey("NganhNgheId")]
        public virtual NganhNghe NganhNghe { set; get; }
    }
}