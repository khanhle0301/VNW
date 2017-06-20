using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VNW.Data.Models
{
    [Table("TinTuyenDungTinhs")]
    public class TinTuyenDungTinh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public int TinTuyenDungId { set; get; }

        public int TinhId { set; get; }

        [ForeignKey("TinTuyenDungId")]
        public virtual TinTuyenDung TinTuyenDung { set; get; }

        [ForeignKey("TinhId")]
        public virtual Tinh Tinh { set; get; }
    }
}