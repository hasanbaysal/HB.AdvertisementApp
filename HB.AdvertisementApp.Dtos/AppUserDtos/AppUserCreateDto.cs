using HB.AdvertisementApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Dtos
{
    public class AppUserCreateDto:IDto
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public int GenderId { get; set; }
    }
}
