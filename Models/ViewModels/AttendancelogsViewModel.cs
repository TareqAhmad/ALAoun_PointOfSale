using System;

namespace ALAoun_Pos.Models
{
    public class AttendanceLogsViewModel
    {
        public int logId { get; set; }

        public int userId { get; set; }

        public string? userName {get; set; }

        public DateTime logTime { get; set; }

        public int logTypeId { get; set; }

        public DateTime? ShiftDate { get; set; }

        public string? Notes { get; set; }



    }
}