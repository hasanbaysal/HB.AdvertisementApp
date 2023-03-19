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
    public class ProvidedServiceService:
             Services<ProvidedServiceCreateDto
            ,ProvidedServiceUpdateDto,
            ProvidedServiceListDto,
            ProvidedService>
        ,IProvidedServiceService
    {

      
        public ProvidedServiceService(
            IMapper mapper,
            IValidator<ProvidedServiceUpdateDto> updateValidator,
            IValidator<ProvidedServiceCreateDto> createValidator,
            IUow uow
            ):base(mapper, updateValidator,createValidator,uow)
        {




        }


    }
}
