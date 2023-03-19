using HB.AdvertisementApp.Business.Interfaces;
using HB.AdvertisementApp.Common;
using HB.AdvertisementApp.Dtos;
using HB.AdvertisementApp.Web.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HB.AdvertisementApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]

  
    public class ApplicationController : Controller
    {
        private readonly IAdvertisementService _advertisementService;
        
        public ApplicationController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        public async Task<IActionResult> List()
        {
            var response = await _advertisementService.GetAllAsync();
            
           return this.ResponseView(response);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdvertisementCreateDto dto)
        {
           var response = await _advertisementService.CreateAsync(dto);


            return  this.ResponseRedirectAction(response, "List");
           

          
        }
 
        public async Task<IActionResult> Update(int id)
        {

            var data = await _advertisementService.GetByIdAsync<AdvertisementUpdateDto>(id);


            return this.ResponseView(data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdvertisementUpdateDto dto)
        {
            var data = await _advertisementService.UpdateAsync(dto);

            return this.ResponseRedirectAction(data, "List");
        }
    }
}
