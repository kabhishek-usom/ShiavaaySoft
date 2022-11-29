using ShivaaySoft.Entities;
using ShivaaySoft.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaaySoft.Repositories.Implementations
{
    public class EnquiryRepository : BaseRepository<EnquiryEntity>, IEnquiryRepository
    {
        public EnquiryRepository(AppDbContext context) : base(context)
        {

        }
    }
}
