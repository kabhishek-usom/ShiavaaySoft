using Microsoft.EntityFrameworkCore;
using ShivaaySoft.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShivaaySoft.Repositories.Implementations
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _db;
        private DbSet<TEntity> _dbSet;
        public BaseRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = _dbSet;
            return query.ToList();
        }

       

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
