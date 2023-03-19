using HB.AdvertisementApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Dtos
{
    public class AdvertisementCreateDto:IDto
    {
        public string Title { get; set; } = null!;
        public bool Status { get; set; }
        public string Description { get; set; } = null!;
    }
}
