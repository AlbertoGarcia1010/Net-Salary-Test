using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Salary.App.Utils;
using Salary.Models.DBContext;
using Salary.Models.Entities;

namespace Salary.Models.Data
{
    public interface IIncreaseData
    {
        Task Insert(Increment increase);
        ListDataModel List();
    }
    public class IncreaseData: IIncreaseData
    {
        private readonly ILogger<IncreaseData> logger;
        private readonly AppDBContext appDBContext;
        private readonly DbContextOptions<AppDBContext> _dbContextOptions;


        public IncreaseData(ILogger<IncreaseData> logger, AppDBContext appDBContext,
            DbContextOptions<AppDBContext> dbContextOptions)
        {
            this.logger = logger;
            this.appDBContext = appDBContext;
            _dbContextOptions = dbContextOptions;
        }

        public async Task Insert(Increment increase)
        {
            Console.WriteLine("Insert DepartmentID: "+increase.DepartmentID);
            Console.WriteLine("Insert IncrementPercent: " + increase.IncrementPercent);
            decimal IncrementPercent = increase.IncrementPercent;
            int isInserted = 0;
            // using var transaction = await appDBContext.Database.BeginTransactionAsync();
            using var context = new AppDBContext(_dbContextOptions);
            try
            {

                await context.Database
                    .ExecuteSqlRawAsync(
                        "EXEC InsertPercentIncrease @vDepartmentID = {0}, @vPercentIncrement = {1}",
                        increase.DepartmentID,
                        IncrementPercent
                    );

                // await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                Console.WriteLine("Error Message: " + ex.Message);
                Console.WriteLine("Error Exception Message: " + ex.InnerException?.Message); //
                // await transaction.RollbackAsync();
                throw;
            }
        }

        public ListDataModel List()
        {
            var result = appDBContext.Increments
                .Join(appDBContext.Departments,
                i => i.DepartmentID,
                d => d.DepartmentID,
                (i, d) => new { i, d })
                .Select((combined) => new
                {
                    combined.i.IncrementPercent,
                    combined.d.Name
                })
                .ToList();
            int totalrows = result.Count;
            int totalrowsafterfiltering = result.Count;
            return new ListDataModel(result, totalrows, totalrowsafterfiltering);
        }

    }
}
