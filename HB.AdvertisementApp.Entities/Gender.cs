using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Entities
{
    public class Gender:BaseEntity
    {
        public string Definition { get; set; } = null!;

        public List<AppUser> AppUsers { get; set; }
    }
}
