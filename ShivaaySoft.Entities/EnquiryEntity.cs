using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShivaaySoft.Entities
{
    public class EnquiryEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }

        [ForeignKey("EnquiryType")]
        public int EnquiryTypeId { get; set; }
        public EnquiryTypeEntity EnquiryType { get; set; }
    }
}
