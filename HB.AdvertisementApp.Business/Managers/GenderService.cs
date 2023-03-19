using AutoMapper;
using FluentValidation;
using HB.AdvertisementApp.Business.Interfaces;
using HB.AdvertisementApp.DataAccess.UnitOfWork;
using HB.AdvertisementApp.Dtos;
using HB.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Business.Managers
{
    public class GenderService:Services<GenderCreateDto,GenderUpdateDto,GenderListDto,Gender>,IGenderService
    {

        public GenderService(IMapper mapper,IValidator<GenderUpdateDto> genderUpdateValidator, IValidator<GenderCreateDto> genderCreateValidator,IUow uow)
            :base(mapper,genderUpdateValidator,genderCreateValidator,uow)
        {


        }

    }
}
