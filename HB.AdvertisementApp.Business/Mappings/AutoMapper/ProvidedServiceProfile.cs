using AutoMapper;
using HB.AdvertisementApp.Dtos;
using HB.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Business.Mappings.AutoMapper
{
    public class ProvidedServiceProfile:Profile
    {
        public ProvidedServiceProfile()
        {
            CreateMap<ProvidedService, ProvidedServiceUpdateDto>().ReverseMap();
            CreateMap<ProvidedService, ProvidedServiceCreateDto>().ReverseMap();
            CreateMap<ProvidedService, ProvidedServiceListDto>().ReverseMap();
        }


    }
}
