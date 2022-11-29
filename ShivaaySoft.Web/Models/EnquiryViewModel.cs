using System;
using System.ComponentModel.DataAnnotations;

namespace ShivaaySoft.Web.Models
{
    public class EnquiryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]        
        public DateTime Dob { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int EnquiryTypeId { get; set; }
    }
}
