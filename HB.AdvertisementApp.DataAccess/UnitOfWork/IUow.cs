using HB.AdvertisementApp.DataAccess.Interfaces;
using HB.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.DataAccess.UnitOfWork
{
    public interface IUow
    {
        IRepository<T> GetRepositoy<T>() where T : BaseEntity;
        Task SaveChangesAsync();

    }
}
