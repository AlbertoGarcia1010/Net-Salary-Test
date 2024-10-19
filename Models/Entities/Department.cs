namespace Salary.Models.Entities
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int StatusID { get; set; }

        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }

    }
}
