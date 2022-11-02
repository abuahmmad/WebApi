using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi1.data;

namespace WebApi1.service
{
    public interface IEmployeeServices
    {
        void Insert(Employee emp);
    }
}
