using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.AdvertisementApp.Common.Enums
{
    public enum OrderByTypes
    {     
        
        ASC=1,
        DESC=2    
    }
    public enum RoleType:int
    {
        Admin=1,
        Member=2
    }
    public enum MilitaryStatusType
    {
        Yapıldı=1,
        Tecilli=2,
        Muaf=3
    }

    public enum AdvertisementAppUserStatusType
    {
        Başvurdu = 1,
        Mülakat = 2,
        Olumsuz = 3
    }


    public enum GenderType
    {
        Erkek = 1,
        Kadın= 2
        
    }
}
