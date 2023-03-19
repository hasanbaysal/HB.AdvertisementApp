using HB.AdvertisementApp.Common.Enums;
using HB.AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.DataAccess.Interfaces
{
    public interface IRepository<T> where T:BaseEntity
    {
         Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync<Tkey>(Expression<Func<T, Tkey>> selector, OrderByTypes orderByTypes = OrderByTypes.ASC);
        Task<List<T>> GetAllAsync<Tkey>(Expression<Func<T, bool>> filter, Expression<Func<T, Tkey>> selector, OrderByTypes orderByTypes = OrderByTypes.ASC);
        Task<T?> FindAsync(object id);
        Task<T?> GetbyFilter(Expression<Func<T, bool>> filter, bool asNoTracking = false);
        IQueryable<T> GetQueryable();
        void Remove(T entity);
        void Create(T entity);
        void Update(T entity, T unchanged);



    }
}
