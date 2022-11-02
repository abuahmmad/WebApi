using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi1.data;

namespace WebApi1.service
{
    public class EmployeeServices: IEmployeeServices
    {
        private readonly HRcontext hRcontext;
        
        public EmployeeServices(HRcontext _hRcontext)
        {
            hRcontext = _hRcontext;
        }

        public void Insert(Employee emp)
        {
            hRcontext.employee.Add(emp);
            hRcontext.SaveChanges();
        }
    }
}
