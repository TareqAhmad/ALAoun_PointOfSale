using System;
using Microsoft.Data.SqlClient;
using ALAoun_Pos.Models; 
using ALAoun_Pos.Services.interfaces; 
using ALAoun_Pos.Data;
using System.Data;


namespace ALAoun_Pos.Services
{
      public class PosPointsService(DbHelper dbHelper) : IPosPointsService
    {
        
       public List<ClsPosPoints> GetAllPosPoints(int CompanyId,int BranchId)
        {
            List<ClsPosPoints> PosPoints = new List<ClsPosPoints>(); 

            string query = @"SELECT PosId, PosName,PosCode
                            FROM PosPoints
                            WHERE IsActive = 1
                            AND CompanyId = @CompanyId
                            AND BranchId = @BranchId;"; 


            SqlParameter[] parameters =
            {
                new SqlParameter("@CompanyId",CompanyId),
                new SqlParameter("@BranchId",BranchId)
            }; 


            DataTable dt = dbHelper.Select(query,parameters); 

            foreach(DataRow row in dt.Rows)
            {
                var PosPoint = new ClsPosPoints
                {
                    PosId = Convert.ToInt32(row[0]),
                    PosName = row[1].ToString(),
                    PosCode = row[2].ToString()
                }; 


                PosPoints.Add(PosPoint); 
            }

            return PosPoints;  
        }

       public ClsPosPoints GetPosPointById(int CompanyId,int BranchId,int PosId)
        {
            var PosPoint = new ClsPosPoints(); 

            return PosPoint; 
        }

        public bool AddPosPoint(ClsPosPoints PosPoint)
        {
            return false; 
        }

        public bool EditPosPoint(ClsPosPoints PosPoint)
        {
              return false; 
        }

        public bool DeletePosPoint(ClsPosPoints PosPoint)
        {
              return false; 
        }


    }


}


