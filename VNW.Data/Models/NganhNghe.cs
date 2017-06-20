using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VNW.Data.Models
{
    [Table("NganhNghes")]
    public class NganhNghe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string Ten { set; get; }

        [MaxLength(256)]
        public string Alias { set; get; }

        public bool Status { set; get; }

        public virtual IEnumerable<TinTuyenDungNganhNghe> TinTuyenDungNganhNghes { set; get; }
    }
}