using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Entities
{
    public class ProvidedService:BaseEntity
    {
        public string Title { get; set; } = null!;
        public string ImagePath { get; set; }  = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;



    }
}
