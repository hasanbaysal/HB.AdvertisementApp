using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Entities
{
    public class Advertisement:BaseEntity
    {

        public string Title { get; set; } = null!;
        public bool Status { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        public List<AdvertisementAppUser> AdvertisementAppUsers { get; set; }

    }
}
