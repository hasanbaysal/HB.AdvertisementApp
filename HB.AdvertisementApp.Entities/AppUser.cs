using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Entities
{
    public class AppUser:BaseEntity
    {


        public string Firstname { get; set; }
        public string Surname { get; set; }

        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        public List<AppUserRole> AppUserRoles { get; set; }

        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        public List<AdvertisementAppUser> AdvertisementAppUsers { get; set; }

    }
}
