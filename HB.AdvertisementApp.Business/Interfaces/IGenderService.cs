﻿using HB.AdvertisementApp.Dtos;
using HB.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Business.Interfaces
{
    public interface IGenderService:IService<GenderCreateDto,GenderUpdateDto,GenderListDto,Gender>
    {
    }
}
