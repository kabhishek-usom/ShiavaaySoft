using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShivaaySoft.Entities
{
    [Table("EnquiryType")]
    public class EnquiryTypeEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.UtcNow.ToLocalTime();
    }
}
