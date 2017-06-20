using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VNW.Data.Models
{
    [Table("CongTyPhucLois")]
    public class CongTyPhucLoi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public int CongTyId { set; get; }

        public int PhucLoiId { set; get; }

        [ForeignKey("CongTyId")]
        public virtual CongTy CongTy { set; get; }

        [ForeignKey("PhucLoiId")]
        public virtual PhucLoi PhucLoi { set; get; }
    }
}