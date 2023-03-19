using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Common
{
    public class Response<T>:Response,IResponse<T>
    {
        public T Data { get; set; } 
        public List<CustomValidationError> Errors { get; set; } 


      
        public Response(ResponseType responseType,string message) :base(responseType,message)
        {
           
        }
        public Response(ResponseType responseType,T data):base(responseType)
        {
            Data = data;
        }
       
        //validation error ve validation error messajları
        public Response(T data,List<CustomValidationError> errors):this(ResponseType.ValidationError,data)
        {
            Errors = errors;
        }


    }
}
