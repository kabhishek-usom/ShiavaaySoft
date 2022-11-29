using ShivaaySoft.Entities;
using ShivaaySoft.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaaySoft.Repositories.Interfaces
{
    public interface IEnquiryTypeRepository : IBaseRepository<EnquiryTypeEntity>
    { 
        //List<EnquiryTypeEntity> filterByAsc(string serachvalue, string sortColumnName, int start, int length);
    }
}
