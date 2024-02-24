using database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace database.Data
{
    public class AdminData
    {
        string connectionString;
        public AdminData()
        {
            ConnectionString connection = new ConnectionString();
            connectionString = connection.Connection;
        }
        public bool validation(Admin NewLogin) 
        {
            int result = 0;
            string _Name = NewLogin.ID;
            string _Password = NewLogin.Password;

            using (var connection = new SqlConnection(connectionString))
            {


                
                string query = $"SELECT ID,Password FROM [Login] WHERE ID = '{_Name}' AND Password = '{_Password}'";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                connection.Open();
                //sqlCommand.CommandType = CommandType.Text;
                //result = Convert.ToInt32(sqlCommand.ExecuteNonQuery());
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {

                    result = 1;

                }


            }
            return result > 0;
        }

    }
}
