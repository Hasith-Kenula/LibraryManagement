using database.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database.Data
{
    public class ReturnData
    {
        string connectionString;
        public ReturnData() 
        {
            ConnectionString connection = new ConnectionString();
            connectionString = connection.Connection;
        }
        public void AddReturnBook(Return _returnBook) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"INSERT INTO [Return] VALUES({_returnBook.BorrowID},{_returnBook.BookID},{_returnBook.MemberID},'{_returnBook.BorrowDate}','{_returnBook.ReturnedDate}')";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public List<Return> GetAllReturnBooks() 
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM [Return]";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Return> returnDetails = new List<Return>();   
                    while (reader.Read()) 
                    {
                        DateTime _borrowDate = new DateTime();
                        DateTime _returnDate = new DateTime();

                        _borrowDate = (DateTime)reader["BorrowDate"];
                        _returnDate = (DateTime)reader["ReturnedDate"];

                        var returnDetail = new Return() 
                        {
                            ID = (int)reader["ID"],
                            BorrowID = (int)reader["BorrowID"],
                            BookID = (int)reader["BookID"],
                            MemberID = (int)reader["MemberID"],
                            BorrowDate = _borrowDate.ToString("yyyy-MM-dd"),
                            ReturnedDate = _returnDate.ToString("yyyy-MM-dd")
                        };
                        returnDetails.Add(returnDetail);
                    }
                    return returnDetails;
                }
            }
        }
    }
}
