using AutoMapper;
using FluentValidation;
using HB.AdvertisementApp.Business.Interfaces;
using HB.AdvertisementApp.Common;
using HB.AdvertisementApp.Common.Enums;
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
    public class AdvertisementService : Services<AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementListDto, Advertisement>, IAdvertisementService
    {
       


        private readonly IUow _uow;
        private readonly IMapper _mapper;

        public AdvertisementService(
             IMapper mapper,
            IValidator<AdvertisementUpdateDto> updateValidator,
            IValidator<AdvertisementCreateDto> createValidator,
            IUow uow) : base(mapper, updateValidator, createValidator, uow)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResponse<List<AdvertisementListDto>>> GetActivesAsync()
        {

            var data = await _uow.GetRepositoy<Advertisement>().GetAllAsync(x => x.Status == true, x => x.CreatedDate, OrderByTypes.DESC);

            var mapperdata = _mapper.Map<List<AdvertisementListDto>>(data);

            return new Response<List<AdvertisementListDto>>(ResponseType.Success,mapperdata);

        }
    }
}
