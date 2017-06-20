using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VNW.Data.Models
{
    [Table("CapBacs")]
    public class CapBac
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string Ten { set; get; }

        public bool Status { set; get; }

        public virtual IEnumerable<TinTuyenDung> TinTuyenDungs { set; get; }
    }
}