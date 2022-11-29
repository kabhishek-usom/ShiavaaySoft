using Microsoft.EntityFrameworkCore;
using ShivaaySoft.Entities;
using ShivaaySoft.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaaySoft.Repositories.Implementations
{
    public class EnquiryTypeRepository : BaseRepository<EnquiryTypeEntity>, IEnquiryTypeRepository
    {
        //private readonly AppDbContext _context;
        //private DbSet<EnquiryTypeEntity> _dbSet;
        public EnquiryTypeRepository(AppDbContext context) : base(context)
        {
            //_context = context;
            //_dbSet = _context.Set<EnquiryTypeEntity>();
        }

        //public List<EnquiryTypeEntity> filterByAsc(string serachvalue, string sortColumnName, int start, int length)
        //{
        //    List<EnquiryTypeEntity> query = _dbSet;
        //    return  query.EnquiryTypes
        //            .Where(a => a..Contains(searchValue) || a.Firstname.Contains(searchValue))
        //            .OrderBy(x => x.GetType().GetProperty(sortColumnName).GetValue(x))//Sort by sortColumn
        //            .Skip(start)
        //            .Take(length)
        //            .ToList();
        //}
    }
}
