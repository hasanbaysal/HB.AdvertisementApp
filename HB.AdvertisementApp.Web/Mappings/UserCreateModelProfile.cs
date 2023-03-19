using AutoMapper;
using HB.AdvertisementApp.Dtos;
using HB.AdvertisementApp.Web.Models;

namespace HB.AdvertisementApp.Web.Mappings
{
    public class UserCreateModelProfile:Profile
    {
        public UserCreateModelProfile()
        {
            CreateMap<UserCreateModel, AppUserCreateDto>().ReverseMap();

        }

    }
}
