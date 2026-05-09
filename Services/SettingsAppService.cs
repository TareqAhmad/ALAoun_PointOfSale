


using System.Data;
using ALAoun_Pos.Data;
using ALAoun_Pos.Models;
using ALAoun_POS.Models;
using ALAoun_POS.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace ALAoun_POS.Services
{
    public class SettingsAppService : ISettingsAppService
    {
        private readonly DbHelper _dbHelper;

        public SettingsAppService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }


        public List<ClsSettingsApp> GetAllSettingsApp(int companyId, int branchId ,int posId = 0)
        {
           var settingsAppList = new List<ClsSettingsApp>();

           string query = "SELECT * FROM Settings WHERE CompanyId = @CompanyId OR (BranchId = @BranchId AND posId = @posId)";

            SqlParameter[] parameters = {
                new SqlParameter("@CompanyId", companyId),
                new SqlParameter("@BranchId", branchId),
                new SqlParameter("@posId", posId)
            };
    
           DataTable dt = _dbHelper.Select(query,parameters); 

           foreach(DataRow row in dt.Rows)
            {
                var setting = new ClsSettingsApp
                {
                    settingId = Convert.ToInt32(row["settingId"]),
                    settingKey = row["settingKey"]?.ToString(),
                    settingValue = row["settingValue"]?.ToString(),
                    settingType = row["settingType"]?.ToString(),
                    
                    // للقيم المنطقية التي قد تكون نول
                    IsEditable = row["IsEditable"] != DBNull.Value && Convert.ToBoolean(row["IsEditable"]),
                    
                    Description = row["Description"]?.ToString(),

                    // للأرقام التي تقبل NULL (إرجاع 0 أو null حسب تعريف الكلاس لديك)
                    CompanyId = row["CompanyId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CompanyId"]),
                    BranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]),
                    posId = row["posId"] == DBNull.Value ? 0 : Convert.ToInt32(row["posId"])
                };
                settingsAppList.Add(setting);

            }

            return settingsAppList;
        }


        public ClsSettingsApp GetSettingsAppById(int companyId, int branchId, int categoryId)
        {
            return new ClsSettingsApp();
        }

        public bool AddSettingApp(ClsSettingsApp settingApp)
        {
            return false;
        }

        public bool EditSettingApp(ClsSettingsApp settingApp)
        {
            return false;
        }

        public bool DeleteSettingApp(ClsSettingsApp settingApp)
        {
            return false;
        }
        

    }
}