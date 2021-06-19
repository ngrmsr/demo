using Demo.DAL.Repositories.Common;
using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
    }
}

