using System.ComponentModel.DataAnnotations;

namespace VNW.Web.Models
{
    public class TinhViewModel
    {
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string Ten { set; get; }

        [MaxLength(256)]
        public string Alias { set; get; }

        public bool Status { set; get; }
    }
}