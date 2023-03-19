using HB.AdvertisementApp.Dtos.Interfaces;

namespace HB.AdvertisementApp.Dtos
{
    public class GenderListDto : IDto
    {
        public int Id { get; set; }
        public string Definition { get; set; } = null!;
    }
}
