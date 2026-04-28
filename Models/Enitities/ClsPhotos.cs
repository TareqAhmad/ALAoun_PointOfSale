using System;

namespace ALAoun_Pos.Models
{
    public class ClsPhotos
    {
        public int PhotoId { get; set; }

        public byte[]? Photo { get; set; }

        public string? PhotoPath { get; set; }

        public string? PhotoName { get; set; }

        public string? PhotoType { get; set; }

        public DateTime UploadDate { get; set; }

        public int? CategoryId { get; set; }

        public int? ProductId { get; set; }

        public int? EmployeeId { get; set; }


        // Navigation Properties
        public ClsCategories? Category { get; set; }

        public ClsProducts? Product { get; set; }

        public ClsEmployees? Employee { get; set; }
    }
}