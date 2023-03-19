using HB.AdvertisementApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Dtos
{
    public class ProvidedServiceUpdateDto:IUpdateDto,IDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
        public string Description { get; set; } = null!;
     
    }
}
