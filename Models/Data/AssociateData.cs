using Microsoft.EntityFrameworkCore;
using Salary.App.Utils;
using Salary.Models.DBContext;
using Salary.Models.Entities;
using Salary.Models.ViewModel;
using System.Text.Json;

namespace Salary.Models.Data
{
    public interface IAssociateData
    {
        ListDataModel List();
        Associate Get(string number);
        Boolean Insert(Associate associate);
        Boolean Edit(Associate associate);
        Boolean Delete(Associate associate);
    }
    public class AssociateData: IAssociateData
    {
        private readonly ILogger<AssociateData> logger;
        private readonly AppDBContext appDBContext;

        public AssociateData(ILogger<AssociateData> logger, AppDBContext appDBContext)
        {
            this.logger = logger;
            this.appDBContext = appDBContext;
        }
        
        public ListDataModel List()
        {
            var result = from a in appDBContext.Associates
                         join d in appDBContext.Departments on a.DepartmentID equals d.DepartmentID
                         join i in appDBContext.Increments on d.DepartmentID equals i.DepartmentID
                        select new
                        {
                            a.AssociateID,
                            a.AssociateNumber,
                            a.AssociateName,
                            a.AssociateFirstLastName,
                            a.AssociateSecondLastName,
                            a.AssociateSalary,
                            a.StatusID,
                            d.DepartmentID,
                            d.Name,
                            i.IncrementPercent,
                            total = a.AssociateSalary + ((a.AssociateSalary * i.IncrementPercent)/100)
                        };
            var res = result.ToList();
            int totalrows = res.Count;
            int totalrowsafterfiltering = res.Count;
            return new ListDataModel(res, totalrows, totalrowsafterfiltering);
        }

        public Associate Get(string number)
        {
            Associate associate = new Associate();
            associate = appDBContext.Associates
                        .Where(a => a.AssociateNumber == number)
                        .FirstOrDefault<Associate>();

            return associate;
        }

        public Boolean Insert(Associate associate)
        {
            Boolean isInserted = false;
            try
            {
                appDBContext.Associates.Add(associate);
                appDBContext.SaveChanges();
                isInserted = true;
            }
            catch (Exception ex)
            {
            }
            return isInserted;
        }

        public Boolean Edit(Associate associate)
        {
            Boolean isEdited = false;
            try
            {
                appDBContext.Entry(associate).State = EntityState.Modified;
                appDBContext.SaveChanges();
                isEdited = true;
            }
            catch (Exception ex)
            {
            }
            return isEdited;
        }

        public Boolean Delete(Associate associate)
        {
            Boolean isDeleted = false;
            try
            {
                appDBContext.Associates.Remove(associate);
                appDBContext.SaveChanges();
                isDeleted = true;
            }

            catch (Exception ex)
            {
                isDeleted = false;
            }
            return isDeleted;
        }
    }
}
