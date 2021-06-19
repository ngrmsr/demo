using Demo.DAL.DbContexts;
using Demo.DAL.Repositories.Common;
using Demo.Models;
using Microsoft.Extensions.Logging;

namespace Demo.DAL.Repositories
{
    public sealed class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext appDbContext, ILoggerFactory loggerFactory)
           : base(appDbContext, loggerFactory)
        {
        }

        //----
    }
}

