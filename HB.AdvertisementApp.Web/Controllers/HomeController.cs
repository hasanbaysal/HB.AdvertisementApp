using HB.AdvertisementApp.Business.Interfaces;
using HB.AdvertisementApp.Dtos;
using HB.AdvertisementApp.Web.Extentions;
using HB.AdvertisementApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HB.AdvertisementApp.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProvidedServiceService _providedService;
        private readonly IAdvertisementService _advertisementService;
        public HomeController(IProvidedServiceService providedService, IAdvertisementService advertisementService)
        {
            _providedService = providedService;
            _advertisementService = advertisementService;
        }

        public async Task<IActionResult> Index()
        {

            var data = await _providedService.GetAllAsync();
            return this.ResponseView(data);
            
        }

        public async Task<IActionResult> HumanResource()
        {
            var data = await _advertisementService.GetActivesAsync();


            return this.ResponseView(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}