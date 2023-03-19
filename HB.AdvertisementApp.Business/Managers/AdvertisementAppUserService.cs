using AutoMapper;
using FluentValidation;
using HB.AdvertisementApp.Business.Extentions;
using HB.AdvertisementApp.Business.Interfaces;
using HB.AdvertisementApp.Common;
using HB.AdvertisementApp.Common.Enums;
using HB.AdvertisementApp.DataAccess.UnitOfWork;
using HB.AdvertisementApp.Dtos;
using HB.AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Business.Managers
{
    public class AdvertisementAppUserService : IAdvertisementAppUserService
    {
        private readonly IUow _uow;
        private readonly IValidator<AdvertisementAppUserCreateDto> _createvalidator;
        private readonly IMapper _mapper;
        public AdvertisementAppUserService(IUow uow, IValidator<AdvertisementAppUserCreateDto> createvalidator, IMapper mapper)
        {
            _uow = uow;
            _createvalidator = createvalidator;
            _mapper = mapper;
        }

        public  async  Task<IResponse<AdvertisementAppUserCreateDto>> CreateAsync(AdvertisementAppUserCreateDto dto)
        {

            var validationResult = _createvalidator.Validate(dto);

            if (validationResult.IsValid)
            {

                var control = await _uow.GetRepositoy<AdvertisementAppUser>()
                                 .GetbyFilter(x => x.AppUserId == dto.AppUserId &&
                                x.AdvertisementId == dto.AdvertisementId);
                if (control !=null)
                {
                    return new Response<AdvertisementAppUserCreateDto>(dto, new() { new() { ErorrMessage = "bu ilana daha önce başvurdunuz", ProppertyName = "" } });
                }

                var createdUser = _mapper.Map<AdvertisementAppUser>(dto);


                _uow.GetRepositoy<AdvertisementAppUser>().Create(createdUser);
              await  _uow.SaveChangesAsync();
 

                return new Response<AdvertisementAppUserCreateDto>
                       (ResponseType.Success,dto);
            }

            return new Response<AdvertisementAppUserCreateDto>
                    (dto, validationResult.CustomErrorList());

        }







        public async Task<List<AdvertisementAppUserListDto>> GetList(AdvertisementAppUserStatusType type)
        {
            var query = _uow.GetRepositoy<AdvertisementAppUser>().GetQueryable();
         
           var data= await query.Include(x => x.AppUser)
                .ThenInclude(x=>x.Gender)
                .Include(x => x.Advertisement)
                .Include(x => x.AdvertisementAppUserStatus)
                .Include(x => x.MilitaryStatus)
                .Where(x => x.AdvertisementAppUserStatusId == (int)type).ToListAsync();


            return _mapper.Map<List<AdvertisementAppUserListDto>>(data);

        }

        public async Task SetStatus(int advertisementAppUserId, int type)
        {
           


            var query = _uow.GetRepositoy<AdvertisementAppUser>().GetQueryable();
            var data = query.SingleOrDefault(x => x.Id == advertisementAppUserId);
            data.AdvertisementAppUserStatusId = type;
           await _uow.SaveChangesAsync();
        }

    }

   
}
