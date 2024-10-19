using Salary.Models.Entities;
using System.ComponentModel;

namespace Salary.Models.ViewModel
{
    public class DepartmentVM: Department
    {
        [DisplayName("Código")]
        public virtual string Code { get; set; } = null!;

        [DisplayName("Nombre")]
        public virtual string Name { get; set; } = null!;

    }
}
