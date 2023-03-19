using HB.AdvertisementApp.Dtos.Interfaces;

namespace HB.AdvertisementApp.Dtos
{
    public class GenderUpdateDto:IUpdateDto
    {
        public int Id { get; set; }
     
        public string Definition { get; set; } = null!;
    }
}
