using HB.AdvertisementApp.Dtos.Interfaces;

namespace HB.AdvertisementApp.Dtos
{
    public class AppRoleUpdateDto:IUpdateDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }
    }
}
