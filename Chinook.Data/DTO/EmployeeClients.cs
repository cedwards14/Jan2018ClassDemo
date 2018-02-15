using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional name spaces
using Chinook.Data.POCOs;
#endregion

namespace Chinook.Data.DTO
{
    public class EmployeeClients
    {
        public string fullname { get; set; }
        public int Client { get; set; }
        public IEnumerable<Clientinfo> ClientList { get; set; }
    }
}
