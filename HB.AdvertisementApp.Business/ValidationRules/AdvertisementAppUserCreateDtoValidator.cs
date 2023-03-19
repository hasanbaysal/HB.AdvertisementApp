using FluentValidation;
using HB.AdvertisementApp.Common.Enums;
using HB.AdvertisementApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Business.ValidationRules
{
    public class AdvertisementAppUserCreateDtoValidator:AbstractValidator<AdvertisementAppUserCreateDto>
    {
        public AdvertisementAppUserCreateDtoValidator()
        {
            RuleFor(x => x.AdvertisementAppUserStatusId).NotEmpty();
            RuleFor(x => x.AdvertisementId).NotEmpty();
            RuleFor(x => x.AppUserId).NotEmpty();
            RuleFor(x => x.CvPath).NotEmpty().WithMessage("bir cv dosyası seçiniz").OverridePropertyName("CvPath");
            RuleFor(x => x.EndDate).NotEmpty().When(x => x.MilitaryStatusId == (int)MilitaryStatusType.Tecilli).WithMessage("tecil tarihi boş olamaz");
            RuleFor(x => x.WorkExperience).NotEmpty().WithMessage("iş tecrübesi giriniz");
           

        }
    }
}
