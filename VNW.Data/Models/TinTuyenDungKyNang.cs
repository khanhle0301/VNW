using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VNW.Data.Models
{
    [Table("TinTuyenDungKyNangs")]
    public class TinTuyenDungKyNang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public int TinTuyenDungId { set; get; }

        public int KyNangId { set; get; }

        [ForeignKey("TinTuyenDungId")]
        public virtual TinTuyenDung TinTuyenDung { set; get; }

        [ForeignKey("KyNangId")]
        public virtual KyNang KyNang { set; get; }
    }
}