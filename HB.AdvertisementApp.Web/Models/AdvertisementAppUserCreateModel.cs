using HB.AdvertisementApp.Common.Enums;

namespace HB.AdvertisementApp.Web.Models
{
    public class AdvertisementAppUserCreateModel
    {
        public int AdvertisementId { get; set; }
        public int AppUserId { get; set; }
        public int AdvertisementAppUserStatusId { get; set; } = (int)AdvertisementAppUserStatusType.Başvurdu;
        public int MilitaryStatusId { get; set; }
        public DateTime? EndDate { get; set; }

        public int WorkExperience { get; set; }
        public IFormFile CvFile { get; set; }

    }
}
