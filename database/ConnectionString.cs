using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database
{
    public class ConnectionString
    {
        public string Connection { get; set; }
        public ConnectionString()
        {
            this.Connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\BIT\LibraryManagement\database\LibraryManagementSystem.mdf;Integrated Security=True";
        }

    }
}
