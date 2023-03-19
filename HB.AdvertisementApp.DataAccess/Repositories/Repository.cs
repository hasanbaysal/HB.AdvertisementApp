using HB.AdvertisementApp.Common.Enums;
using HB.AdvertisementApp.DataAccess.Contexts;
using HB.AdvertisementApp.DataAccess.Interfaces;
using HB.AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.DataAccess.Repositories
{
    public class Repository<T>:IRepository<T> where T : BaseEntity
    {
        private readonly AdvertisementAppDbContext _context;

        public Repository(AdvertisementAppDbContext context)
        {
            _context = context;
        }

        public  async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T,bool>> filter)
        {
            return await _context.Set<T>().AsNoTracking().Where(filter).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<Tkey>(Expression<Func<T, Tkey>> selector, OrderByTypes orderByTypes =OrderByTypes.ASC )
        {

            return orderByTypes == OrderByTypes.ASC 
                ? await _context.Set<T>().AsNoTracking().OrderBy(selector).ToListAsync() 
                                                    : await _context.Set<T>().AsNoTracking().OrderByDescending(selector).ToListAsync();

        }

        public async Task<List<T>> GetAllAsync<Tkey>(Expression<Func<T, bool>> filter,Expression<Func<T, Tkey>> selector, OrderByTypes orderByTypes = OrderByTypes.ASC)
        {


            var query = _context.Set<T>().AsNoTracking().Where(filter);
            
            return orderByTypes == OrderByTypes.ASC ? 
                    await query.OrderBy(selector).ToListAsync() :
                                    await query.OrderByDescending(selector).ToListAsync();

        }

        public async Task<T?> FindAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<T?> GetbyFilter(Expression<Func<T,bool>> filter,bool asNoTracking= false)
        {

            return !asNoTracking ? await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter) :
                                    await _context.Set<T>().SingleOrDefaultAsync(filter);


        }
        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>().AsQueryable();

        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void Update(T entity,T unchanged)
        {

            _context.Entry(unchanged).CurrentValues.SetValues(entity);
        }
    }
}
