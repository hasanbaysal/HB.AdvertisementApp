using AutoMapper;
using FluentValidation;
using HB.AdvertisementApp.Business.Extentions;
using HB.AdvertisementApp.Business.Interfaces;
using HB.AdvertisementApp.Common;
using HB.AdvertisementApp.DataAccess.UnitOfWork;
using HB.AdvertisementApp.Dtos.Interfaces;
using HB.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Business.Managers
{
    public class Services<CreateDto, UpdateDto, ListDto,T> : IService<CreateDto, UpdateDto, ListDto,T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createDtoValidator;
        private readonly IValidator<UpdateDto> _UpdateDtoValidator;
        private readonly IUow _uow;



        public Services(IMapper mapper, IValidator<UpdateDto> updateDtoValidator, IValidator<CreateDto> createDtoValidator, IUow uow)
        {
            _mapper = mapper;
            _UpdateDtoValidator = updateDtoValidator;
            _createDtoValidator = createDtoValidator;
            _uow = uow;
        }

        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto dto)
        {
            var result = _createDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var converted = _mapper.Map<T>(dto);
                    _uow.GetRepositoy<T>().Create(converted);
                await _uow.SaveChangesAsync();

                return new Response<CreateDto>(ResponseType.Success, dto);
            }

            return new Response<CreateDto>(dto,result.CustomErrorList());
        }

        public async Task<IResponse<List<ListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepositoy<T>().GetAllAsync();
            var converted = _mapper.Map<List<ListDto>>(data);
            return new Response<List<ListDto>>(ResponseType.Success,converted);   

        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = await _uow.GetRepositoy<T>().GetbyFilter(x => x.Id == id);
            
            if (data == null)
                
                return new Response<IDto>(ResponseType.NotFound,$"{id} id data bulunamadı");

                    var mappedData= _mapper.Map<IDto>(data);

            return new Response<IDto>(ResponseType.Success,mappedData);

        }

        public async Task<IResponse> RemoveAsync(int id)
        {

            var data =await _uow.GetRepositoy<T>().FindAsync(id);
            if (data == null)
            
                return new Response(ResponseType.NotFound,$"{id} id data bulunamadı");
            
                 _uow.GetRepositoy<T>().Remove(data);
                 await _uow.SaveChangesAsync();
            
                return new Response(ResponseType.Success);
               
        }

        public async Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto)
        {
            
            var validation =_UpdateDtoValidator.Validate(dto);
            if (!validation.IsValid)
                return new Response<UpdateDto>(dto,validation.CustomErrorList());

            var data = await _uow.GetRepositoy<T>().FindAsync(dto.Id);

            if (data == null)
                return new Response<UpdateDto>(ResponseType.NotFound, $"{dto.Id} id data bulunamadı ");


            var mappedData = _mapper.Map<T>(dto);

             _uow.GetRepositoy<T>().Update(mappedData, data);
            await _uow.SaveChangesAsync();

            return new Response<UpdateDto>(ResponseType.Success,dto);
                

        }
    }
}
