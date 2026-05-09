
using Microsoft.Data.SqlClient;
using System.Data;
using ALAoun_Pos.Models;
using ALAoun_Pos.Data;
using ALAoun_Pos.Services.interfaces;      

namespace ALAoun_Pos.Services
{
    public class UnitsService : IUnitsService
    {
        private readonly DbHelper _dbHelper;
        public UnitsService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
      public List<ClsUnits> GetAllUnits(int companyId,int branchId)
       { 

          List<ClsUnits> units = new List<ClsUnits>();
            
            string query = @"SELECT unitId,unitName
                           FROM Units
                           WHERE companyId =@companyId AND branchId =@branchId"; 

           SqlParameter[] parameters=
            {
                new SqlParameter("@companyId",companyId),
                new SqlParameter("@branchId",branchId)
            }; 

            DataTable dt = _dbHelper.Select(query,parameters); 

            foreach(DataRow row in dt.Rows)
            {
                var unit = new ClsUnits
                {
                    UnitId = Convert.ToInt32(row[0]),
                    UnitName = row[1].ToString()
                };

                units.Add(unit);
            }

            return units;
        }
           
             

    
     public ClsUnits GetUnitById(int companyId,int branchId,int id)
        {
            ClsUnits unit = null;

            return unit;
        } 
     public bool AddUnit(string unitName,int companyId,int branchId)
    {      

        string query = @"INSERT INTO Units (unitName,companyId,branchId) VALUES (@unitName,@companyId,@branchId)";

        SqlParameter[] parameters =
        {
            new SqlParameter("@unitName", unitName),
            new SqlParameter("@companyId", companyId),
            new SqlParameter("@branchId", branchId)
        };

        int rowsAffected = _dbHelper.Execute(query, parameters);

        return rowsAffected > 0;
    }
     public bool EditUnit(ClsUnits unit)
        {
            return false;
        }
     public bool DeleteUnit(ClsUnits unit)
        {
            return false; 
        }


    }

}