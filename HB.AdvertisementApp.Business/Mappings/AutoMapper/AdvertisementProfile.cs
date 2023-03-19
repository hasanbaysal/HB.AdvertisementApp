﻿using AutoMapper;
using HB.AdvertisementApp.Dtos;
using HB.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Business.Mappings.AutoMapper
{
    public class AdvertisementProfile:Profile
    {
        public AdvertisementProfile()
        {
            CreateMap<Advertisement, AdvertisementCreateDto>().ReverseMap();
            CreateMap<Advertisement, AdvertisementUpdateDto>().ReverseMap();
            CreateMap<Advertisement, AdvertisementListDto>().ReverseMap();

        }
    }
}
