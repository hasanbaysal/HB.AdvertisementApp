using HB.AdvertisementApp.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HB.AdvertisementApp.Web.Extentions
{
    public static class ControllerExtention
    {

        public static IActionResult  ResponseRedirectAction<T>(this Controller controller,IResponse<T> response,string actionName,string ControllerName="")
        {

            if(response.ResponseType == ResponseType.NotFound)
            {
                return controller.NotFound();
            }
            if (response.ResponseType == ResponseType.ValidationError)
            {
               
                response.Errors.ForEach(x => controller.ModelState.AddModelError(x.ProppertyName, x.ErorrMessage));
                return controller.View(response.Data);
            }
            if (string.IsNullOrWhiteSpace(ControllerName))
            {
                return controller.RedirectToAction(actionName);
            }
            return controller.RedirectToAction(actionName,ControllerName);
           
        }
        public static IActionResult ResponseView<T>(this Controller controller,IResponse<T> response) 
        {
        
            if (response.ResponseType == ResponseType.NotFound)
            {
                return controller.NotFound();
            }
            else
            {
                return controller.View(response.Data);
            }

        }
        public static IActionResult ResponseView(this Controller controller, IResponse response, string actionName)
        {
         

            if (response.ResponseType==ResponseType.NotFound)
            {
                return controller.NotFound();
            }
            return controller.RedirectToAction(actionName);
        }

    }
}
