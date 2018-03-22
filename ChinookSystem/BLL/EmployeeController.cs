using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChinookSystem.DAL;
using System.ComponentModel;
using Chinook.Data.Entities;
using Chinook.Data.DTO;

namespace ChinookSystem.BLL
{   
    [DataObject]
 public   class EmployeeController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<EmployeeClients> Employee_GetClientList()
        {
            using (var context = new ChinookContext())
            { 
                var results = from x in context.Employees
                              where x.Title.Contains("support")
                              orderby x.LastName, x.FirstName
                              select new EmployeeClients
                              {
                                  fullname = x.LastName + " " + x.FirstName,
                                  //title = x.Title,
                                  Client = x.Customers.Count(),
                                  ClientList = from y in x.Customers
                                               orderby y.LastName, y.FirstName
                                               select new Clientinfo
                                               {
                                                   Client = y.LastName + " " + y.FirstName,
                                                   Phone = y.Phone
                                               }
                              };

                return results.ToList();
                }
        }


        public Employee Employee_Get(int employeeid)
        {
            using (var context = new ChinookContext())
            {
                return context.Employees.Find(employeeid);
            }
        }


    }
}
