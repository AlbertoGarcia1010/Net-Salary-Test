using System.ComponentModel.DataAnnotations;

namespace Salary.Models.Entities
{
    public class Increment
    {
        [Key]
        public int IncrementID { get; set; }
        public int DepartmentID { get; set; }
        public decimal IncrementPercent { get; set; }
        public int StatusID { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
}
