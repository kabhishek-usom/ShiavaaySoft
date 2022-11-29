using System.ComponentModel.DataAnnotations;

namespace ShivaaySoft.Web.Models
{
    public class EnquiryTypeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
