using HB.AdvertisementApp.DataAccess.Contexts;
using HB.AdvertisementApp.DataAccess.Interfaces;
using HB.AdvertisementApp.DataAccess.Repositories;
using HB.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.DataAccess.UnitOfWork
{
    public class Uow:IUow
    {
        private readonly AdvertisementAppDbContext _context;

        public Uow(AdvertisementAppDbContext context)
        {
            _context = context;
        }


        public IRepository<T> GetRepositoy<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
