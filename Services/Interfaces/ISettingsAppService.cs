

using ALAoun_Pos.Models;
using ALAoun_POS.Models;

namespace ALAoun_POS.Services.Interfaces
{
    public interface ISettingsAppService
    {
       
            public List<ClsSettingsApp> GetAllSettingsApp(int companyId ,int branchId,int posId = 0); 

            public ClsSettingsApp GetSettingsAppById(int companyId ,int branchId,int categoryId);

            public bool AddSettingApp(ClsSettingsApp settingApp); 

            public bool EditSettingApp(ClsSettingsApp settingApp); 

            public bool DeleteSettingApp(ClsSettingsApp settingApp); 



    }
}