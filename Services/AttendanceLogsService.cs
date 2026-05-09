
using System.Reflection.Metadata.Ecma335;
using ALAoun_Pos.Models;
using ALAoun_Pos.Data; 
using ALAoun_Pos.Services.interfaces;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
using System.Data;


namespace ALAoun_Pos.Services
{
    public class AttendanceLogsService : IAttendanceLogsService
    {
            
      
        private readonly DbHelper _dbHelper; 

         public AttendanceLogsService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper; 
        }

        public List<AttendanceLogsViewModel> GetAllAttendance(int companyId, int branchId,int posId)
        {
            var listAttendanceLogs = new List<AttendanceLogsViewModel>(); 


            string query = @"SELECT *
                             FROM AttendanceLogs
                             WHERE companyId = @companyId
                             AND branchId = @branchId
                             AND posId = @posId;"; 

            SqlParameter [] parameters =
            {
                new SqlParameter("@companyId",companyId),
                new SqlParameter("@branchId",branchId),
                new SqlParameter("@posId",posId)
            }; 

            DataTable dt = _dbHelper.Select(query,parameters); 

            foreach(DataRow row in dt.Rows)
            {
                var attendance  = new AttendanceLogsViewModel
                {
                    logId = Convert.ToInt32(row[0]),
                    userId = Convert.ToInt32(row[1]),
                    userName = row[2].ToString(),
                    logTime = Convert.ToDateTime(row[3]),
                    logTypeId = Convert.ToInt32(row[4]),
                    ShiftDate = Convert.ToDateTime(row[5]),
                    Notes = row[6].ToString()
                
                }; 
                  listAttendanceLogs.Add(attendance); 

            }

           return listAttendanceLogs; 
        }

        public ClsAttendanceLogs GetAttendanceById(int companyId, int branchId,int posId)
        {
            throw new NotImplementedException();
        }

        public bool AddAttendance(AuditLoginDto auditLoginDto)
        {   
            string query = @"INSERT INTO AttendanceLogs(userId,userName,LogTime,logTypeId,ShiftDate,Notes,CompanyId,BranchId,PosId)
                            VALUES(@userId,@userName,@logTime,@logTypeId,@shitDate,@notes,@companyId,@branchId,@posId)"; 

            SqlParameter[] parameters =
            {
               new SqlParameter("@userId",auditLoginDto.userId),
               new SqlParameter("@userName",auditLoginDto.userName), 
               new SqlParameter("@logTime",auditLoginDto.logTime), 
               new SqlParameter("@logTypeId",auditLoginDto.logTypeId),  
               new SqlParameter("@shitDate",auditLoginDto.shiftDate),  
               new SqlParameter("@notes",auditLoginDto.notes),  
               new SqlParameter("@companyId",auditLoginDto.companyId),    
               new SqlParameter("@branchId",auditLoginDto.branchId), 
               new SqlParameter("@posId",auditLoginDto.posId)

            }; 

             var rowsAffected = _dbHelper.Execute(query,parameters); 

             return rowsAffected > 0;   
        
        }

        public bool EditAttendance(AuditLoginDto auditLoginDto)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBranch(AuditLoginDto auditLoginDto)
        {
            throw new NotImplementedException();
        }

    }

}
