using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HB.AdvertisementApp.Web.Models
{
    public class UserCreateModel
    {

        [Display( Description  ="adınız")]
        public string Firstname { get; set; } = null!;

        [Display(Description = "Soyadınız")]
        public string Surname { get; set; } = null!;
        [Display(Description = "kullanıcı adınız")]
        public string UserName { get; set; } = null!;

        [Display(Description = "Şifreniz")]
        public string Password { get; set; } = null!;
        [Display(Description = "Şifre Tekrar")]
        public string  ConfirmPassword { get; set; } = null!;
        [Display(Description = "Telefon Numara ")]
        public string? PhoneNumber { get; set; } = null!;

        [Display(Description = "Cinsiyetiniz")]
        public int GenderId { get; set; }

        public SelectList Genders { get; set; }
    }
}
