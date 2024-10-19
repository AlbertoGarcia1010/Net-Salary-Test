namespace Salary.Models.Entities
{
    public class Associate
    {
        public int AssociateID { get; set; }
        public int DepartmentID { get; set; }
        public string AssociateNumber { get; set; }
        public string AssociateName { get; set; }
        public string AssociateFirstLastName { get; set; }
        public string AssociateSecondLastName { get; set; }
        public decimal AssociateSalary { get; set; }
        public int StatusID { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
}
