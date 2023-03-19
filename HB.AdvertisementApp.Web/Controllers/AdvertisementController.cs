using HB.AdvertisementApp.Business.Interfaces;
using HB.AdvertisementApp.Common;
using HB.AdvertisementApp.Common.Enums;
using HB.AdvertisementApp.Dtos;
using HB.AdvertisementApp.Entities;
using HB.AdvertisementApp.Web.Extentions;
using HB.AdvertisementApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System.Security.AccessControl;
using System.Security.Claims;

namespace HB.AdvertisementApp.Web.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IAdvertisementAppUserService _advertisementAppUserService;
        private readonly IFileProvider _fileProvider;
        public AdvertisementController(IAppUserService appUserService, IAdvertisementAppUserService advertisementAppUserService, IFileProvider fileProvider)
        {
            _appUserService = appUserService;
            _advertisementAppUserService = advertisementAppUserService;
            _fileProvider = fileProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Member")]
        public async Task<IActionResult> Send(int advertisementId)
        {

            var userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value);


             var userResponse = await _appUserService.GetByIdAsync<AppUserListDto>(userId);

            var userInfo = userResponse.Data;

            ViewBag.GenderId = userInfo.GenderId;

            var items = Enum.GetValues(typeof(MilitaryStatusType));

            var list = new List<MilitaryStatusListDto>();
            foreach (int item in items) 
            {
                list.Add(new MilitaryStatusListDto()
                {
                    Id = item,
                    Definition = Enum.GetName(typeof(MilitaryStatusType), item)!

                }); 
            }

            ViewBag.MilitaryStatus = new SelectList(list,"Id", "Definition");

            return View(new AdvertisementAppUserCreateModel()
            {
                AdvertisementId=advertisementId,
                AppUserId=userInfo.Id,

            });



        }


        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<IActionResult> Send(AdvertisementAppUserCreateModel model)
        {
            AdvertisementAppUserCreateDto dto = new();
            if (model.CvFile !=null)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(model.CvFile.FileName);

                var cvpath= _fileProvider.GetDirectoryContents("wwwroot").First(x => x.Name == "CvFiles");
             
                var path = Path.Combine(cvpath.PhysicalPath!, filename);


                var stream = new FileStream(path,FileMode.Create);

                await model.CvFile.CopyToAsync(stream);
                dto.CvPath = filename;

            }

            dto.AdvertisementId = model.AdvertisementId;
            dto.AdvertisementAppUserStatusId=model.AdvertisementAppUserStatusId;
            dto.EndDate = model.EndDate;
            dto.AppUserId = model.AppUserId;
            dto.WorkExperience = model.WorkExperience;
            dto.MilitaryStatusId = model.MilitaryStatusId;
            


           var response =  await  _advertisementAppUserService.CreateAsync(dto);


            if (response.ResponseType==ResponseType.ValidationError)
            {




                var items = Enum.GetValues(typeof(MilitaryStatusType));

                var list = new List<MilitaryStatusListDto>();
                foreach (int item in items)
                {
                    list.Add(new MilitaryStatusListDto()
                    {
                        Id = item,
                        Definition = Enum.GetName(typeof(MilitaryStatusType), item)!

                    });
                }

                ViewBag.MilitaryStatus = new SelectList(list, "Id", "Definition");



                var userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value);


                var userResponse = await _appUserService.GetByIdAsync<AppUserListDto>(userId);

                var userInfo = userResponse.Data;

                ViewBag.GenderId = userInfo.GenderId;


                response.Errors.ForEach(x => ModelState.AddModelError(x.ProppertyName, x.ErorrMessage));
                return View(model);
            }
            
            return this.ResponseRedirectAction(response, "HumanResource","Home");
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> List()
        {
            var data = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Başvurdu);
                return View(data);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetStatus(int advertisementAppUserId,int type )
        {

          await  _advertisementAppUserService.SetStatus(advertisementAppUserId, type);
            return RedirectToAction("List");
        } 
        public async Task<IActionResult> ApprovedList()
        {
            var data = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Mülakat);
            return View(data);
        }
        public async Task<IActionResult> RejectedList()
        {
            var data = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Olumsuz);
            return View(data);
        }

    }
}
