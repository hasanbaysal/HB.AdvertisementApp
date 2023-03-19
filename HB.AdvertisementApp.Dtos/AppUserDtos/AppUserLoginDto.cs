using HB.AdvertisementApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Dtos
{
    public class AppUserLoginDto:IDto
    {
        public string UserName { get; set;}
        public string Password { get; set;}
        public bool RememberMe { get; set; }
    }
}
