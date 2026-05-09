

using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces
{
    public interface IAttendanceLogsService
    {
        public List<AttendanceLogsViewModel> GetAllAttendance(int companyId,int branchId,int posId);

        public ClsAttendanceLogs GetAttendanceById(int companyId,int branchId,int posId);

        public bool AddAttendance(AuditLoginDto auditLoginDto);

        public bool EditAttendance(AuditLoginDto auditLoginDto);
        public bool DeleteBranch(AuditLoginDto auditLoginDto);
    }
}