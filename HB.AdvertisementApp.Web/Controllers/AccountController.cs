using AutoMapper;
using FluentValidation;
using HB.AdvertisementApp.Business.Interfaces;
using HB.AdvertisementApp.Common;
using HB.AdvertisementApp.Common.Enums;
using HB.AdvertisementApp.Dtos;
using HB.AdvertisementApp.Web.Extentions;
using HB.AdvertisementApp.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HB.AdvertisementApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenderService _genderService;
        private readonly IAppUserService _appUserService;
        private readonly IValidator<UserCreateModel> _userCreateValidator;
        private readonly IValidator<AppUserLoginDto> _appUserLoginDtoValidator;

        public AccountController(IGenderService genderService, IValidator<UserCreateModel> userCreateValidator, IMapper mapper, IAppUserService appUserService, IValidator<AppUserLoginDto> appUserLoginDto)
        {
            _genderService = genderService;
            _userCreateValidator = userCreateValidator;
            _mapper = mapper;
            _appUserService = appUserService;
            _appUserLoginDtoValidator = appUserLoginDto;
        }

        public async Task<IActionResult> SignUp()
        {
            var genders = await _genderService.GetAllAsync();
            var model = new UserCreateModel();
            model.Genders = new SelectList(genders.Data,"Id", "Definition");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateModel vm)
        {

            var result = _userCreateValidator.Validate(vm);

            if (!result.IsValid)
            {
                var genders = await _genderService.GetAllAsync();
                vm.Genders = new SelectList(genders.Data, "Id", "Definition", vm.GenderId);
                result.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
                return View(vm);
            }
          
            var dto = _mapper.Map<AppUserCreateDto>(vm);

            var createResponse = await _appUserService.CreateWithRoleAsync(dto,(int)RoleType.Member);

            return this.ResponseRedirectAction(createResponse, "SignIn");

            
        }


        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLoginDto dto)
        {

        

            var result = await _appUserService.CheckUserAsync(dto);

            if (result.ResponseType == ResponseType.ValidationError)
            {
                ModelState.AddModelError("", result.Message);
                     return View(dto);
            }


            var roleResult = await _appUserService.GetRolesByUserIdAsync(result.Data.Id);
            var claims = new List<Claim>();
            if (roleResult.ResponseType == ResponseType.Success)
            {
                foreach (var item in roleResult.Data)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Definition));
                }

            }
         

            claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()));
     
            
            var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties() { IsPersistent= dto.RememberMe };


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
                , new ClaimsPrincipal(claimsIdentity)
                , authProperties);

            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
        }   

    }
}
