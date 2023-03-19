using FluentValidation;
using HB.AdvertisementApp.Dtos;
using HB.AdvertisementApp.Web.Models;

namespace HB.AdvertisementApp.Web.ValidationRules
{
    public class UserCreateMoldelValidator:AbstractValidator<UserCreateModel>
    {

        public UserCreateMoldelValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("şifre boş olamaz");
            RuleFor(x => x.Password).MinimumLength(3);
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("şifreler aynı olmalı");
            RuleFor(x => x.UserName).MinimumLength(3);
            RuleFor(x => new { x.UserName}).Must(name =>
            {
                return  !name.UserName.Contains("123");
            }).When(a=>a.UserName != null).WithMessage("kullanıcı adı 123 içeremez özel kural").OverridePropertyName(nameof(UserCreateModel.UserName));

            RuleFor(x => new
            {

                username = x.UserName,
                firstname = x.Firstname
            }).Must(uf =>
            {
                return !uf.username.Contains(uf.firstname);
            }).When(x=>x.UserName!=null && x.Firstname!=null).WithMessage("kullanıcı adı isim içeremez");

            RuleFor(x => new
            {
                password = x.Password,
                username = x.UserName
            }).Must(pu =>
            {
                return !pu.password.Contains(pu.username);
            })
            .When(x => x.UserName != null && x.Password != null)
            .WithMessage("şifre kullanıcı adı içeremez");


            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();

            //genderıd ?

        }
    }
}
