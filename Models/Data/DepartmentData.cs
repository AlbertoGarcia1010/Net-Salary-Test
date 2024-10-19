using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Salary.App.Utils;
using Salary.Models.DBContext;
using Salary.Models.Entities;
using Salary.Models.ViewModel;
using System.Collections.Generic;
using System.Text.Json;

namespace Salary.Models.Data
{
    public interface IDepartmentData
    {
        Boolean Insert(Department department);
        Department Get(string id);
        Boolean Edit(Department department);
        Boolean Delete(Department department);
        ListDataModel List();
        List<Department> GetAll();
    }
    public class DepartmentData: IDepartmentData
    {
        private readonly ILogger<DepartmentData> logger;
        private readonly AppDBContext appDBContext;

        public DepartmentData(ILogger<DepartmentData> logger, AppDBContext appDBContext)
        {
            this.logger = logger;
            this.appDBContext = appDBContext;
        }

        public ListDataModel List()
        {
            var result = appDBContext.Departments
                .ToList();
            int totalrows = result.Count;
            int totalrowsafterfiltering = result.Count;
            return new ListDataModel(result, totalrows, totalrowsafterfiltering);
        }

        public List<Department> GetAll()
        {
            List<Department> departments = new List<Department>();
            departments = appDBContext.Departments
                            .OrderBy(x => x.Name)
                            .ToList<Department>();
            return departments;
        }

        public Boolean Insert(Department department)
        {
            Boolean isInserted = false;
            try
            {
                appDBContext.Departments.Add(department);
                appDBContext.SaveChanges();
                isInserted = true;
            }
            catch (Exception ex)
            {
            }
            return isInserted;
        }

        public Department Get(string code)
        {
            Department department = new Department();
            department = appDBContext.Departments
                        .Where(d => d.Code == code)
                        .FirstOrDefault<Department>();

            return department;
        }

        public Boolean Edit(Department department)
        {
            Console.WriteLine("DepartmentData Edit : " + JsonSerializer.Serialize(department));

            Boolean isEdited = false;
            try
            {
                appDBContext.Entry(department).State = EntityState.Modified;
                appDBContext.SaveChanges();
                isEdited = true;
            }
            catch (Exception ex)
            {
            }
            return isEdited;
        }

        public Boolean Delete(Department department)
        {
            Boolean isDeleted = false;
            Console.WriteLine("DepartmentData Delete : " + JsonSerializer.Serialize(department));
            try
            {
                appDBContext.Departments.Remove(department);
                appDBContext.SaveChanges();
                isDeleted = true;
            }
            
            catch (Exception ex)
            {
                logger.LogInformation("ErrorDB|Department"+ ex);

                isDeleted = false;
            }
            return isDeleted;
        }
    }
}
