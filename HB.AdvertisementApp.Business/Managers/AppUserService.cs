using AutoMapper;
using FluentValidation;
using HB.AdvertisementApp.Business.Extentions;
using HB.AdvertisementApp.Business.Interfaces;
using HB.AdvertisementApp.Common;
using HB.AdvertisementApp.DataAccess.Interfaces;
using HB.AdvertisementApp.DataAccess.UnitOfWork;
using HB.AdvertisementApp.Dtos;
using HB.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Business.Managers
{
    public class AppUserService:Services<AppUserCreateDto,AppUserUpdateDto,AppUserListDto,AppUser>,IAppUserService
    {

        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<AppUserCreateDto> _appUserCreateDtoValidator;
        private readonly IValidator<AppUserLoginDto> _appUserLoginDtoValidator;
        public AppUserService(IMapper mapper, IValidator<AppUserUpdateDto> appUserUpdateValidator, IValidator<AppUserCreateDto> appUserCreateValidator, IUow uow, IValidator<AppUserLoginDto> appUserLoginDtoValidator) : base(mapper, appUserUpdateValidator, appUserCreateValidator, uow)
        {
            _uow = uow;
            _mapper = mapper;
            _appUserCreateDtoValidator = appUserCreateValidator;
            _appUserLoginDtoValidator = appUserLoginDtoValidator;
        }

        public async Task<IResponse<AppUserCreateDto>> CreateWithRoleAsync(AppUserCreateDto dto,int roleId)
        {

            var validation =  _appUserCreateDtoValidator.Validate(dto);

            if (validation.IsValid)
            {

                var user = _mapper.Map<AppUser>(dto);

                
                        
                _uow.GetRepositoy<AppUserRole>().Create(new AppUserRole
                {
                    AppUser = user,
                    AppRoleId = roleId
                });


                await _uow.SaveChangesAsync();

                return new Response<AppUserCreateDto>(ResponseType.Success, dto);
            

            }
            return new Response<AppUserCreateDto>(dto, validation.CustomErrorList());


        }

        public async Task<IResponse<AppUserListDto>> CheckUserAsync(AppUserLoginDto dto)
        {

            var validator = _appUserLoginDtoValidator.Validate(dto);
            if (!validator.IsValid)
             {
                return new Response<AppUserListDto>(ResponseType.ValidationError, "Kullanıcı adı veya şifre hatalı boş bırakılamaz");
             }
            var data = await _uow.GetRepositoy<AppUser>()
                .GetbyFilter(x => x.UserName == dto.UserName && x.Password == dto.Password);

            if(data!=null)
            {
                var appUserListDto = _mapper.Map<AppUserListDto>(data);
                return new Response<AppUserListDto>(ResponseType.Success, appUserListDto);
            }

            return new Response<AppUserListDto>(ResponseType.ValidationError, "kullanıcı adı veya şifre hatalı");

            
        }

        public async Task<IResponse<List<AppRoleListDto>>> GetRolesByUserIdAsync(int userId)
        {

         
            var data =await  _uow.GetRepositoy<AppRole>().GetAllAsync(x=>x.AppUserRoles.Any(x=>x.AppUserId ==userId),x=>x.Definition);

       
            if (data == null)
            {

                return new Response<List<AppRoleListDto>>(ResponseType.NotFound, "ilgili rol bulunamadı");
            }
            var mapped = _mapper.Map<List<AppRoleListDto>>(data);

            return new Response<List<AppRoleListDto>>(ResponseType.Success,mapped);
        }

    }
}
