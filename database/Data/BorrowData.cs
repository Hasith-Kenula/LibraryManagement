using database.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace database.Data
{
    public class BorrowData
    {
        string connectionString;
        public BorrowData()
        {
            ConnectionString connection = new ConnectionString();
            connectionString = connection.Connection;
        }
        public string RemainDays(DateTime _returnDate) 
        {
            DateTime today = DateTime.Today;
            TimeSpan remainDate = _returnDate - today;
            if (remainDate.TotalDays < 0)
            {
                return $"Passed {remainDate.TotalDays * (-1)} days";
            }
            else 
            {
                return remainDate.Days.ToString();
            }
            
        }
        public List<Borrow> SelectAllBorrowData() 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = "SELECT * FROM [Borrowing]";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    
                    List<Borrow> borrowings = new List<Borrow>();

                    while (reader.Read()) 
                    {
                        DateTime _borrowDate = new DateTime();
                        DateTime _returnDate = new DateTime();

                        _borrowDate = (DateTime)reader["BorrowDate"];
                        _returnDate = (DateTime)reader["ReturnDate"];
                        
                        var borrow = new Borrow()
                        {
                            ID = (int)reader["ID"],
                            BookID = (int)reader["BookID"],
                            MemberID = (int)reader["MemberID"],
                            BorrowDate = _borrowDate.ToString("yyyy-MM-dd"),
                            ReturnDate = _returnDate.ToString("yyyy-MM-dd"),
                            RemainingDays = RemainDays(_returnDate)
                    };
                        borrowings.Add(borrow);
                    }
                    return borrowings;
                } 
            }
        }
        public bool Reservation(Borrow _borrow)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"INSERT INTO [Borrowing] VALUES('{_borrow.BookID}','{_borrow.MemberID}','{_borrow.BorrowDate}','{_borrow.ReturnDate}')";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool CheckMemberBorrowCount(int _memberID) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"SELECT COUNT(*) AS borrowCount FROM [Borrowing] WHERE MemberID = '{_memberID}'";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    return (int)command.ExecuteScalar() < 2;
                }
            }
        }
        public int GetMemberID(int _BorrowingID) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"SELECT (MemberID) FROM [Borrowing] WHERE ID = '{_BorrowingID}'";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
            }
        }
        public bool DeleteBorrow(Borrow _return) 
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"DELETE FROM [Borrowing] WHERE ID = {_return.ID} AND BookID = {_return.BookID} AND MemberID = {_return.MemberID}";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        public string GetBorrowDate(int ID) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"SELECT BorrowDate FROM [Borrowing] WHERE ID = '{ID}'";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();

                    DateTime borrowDate = new DateTime();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) 
                    {
                        borrowDate = (DateTime)reader["BorrowDate"];
                    }
                    return borrowDate.ToString("yyyy-MM-dd");
                }
            }
        }
        public List<Borrow> SearchBorrowData(string _searchText) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"SELECT * FROM [Borrowing] WHERE MemberID LIKE '{_searchText}%'";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    List<Borrow> borrows = new List<Borrow>();

                    while (reader.Read()) 
                    {
                        DateTime _borrowDate = new DateTime();
                        DateTime _returnDate = new DateTime();

                        _borrowDate = (DateTime)reader["BorrowDate"];
                        _returnDate = (DateTime)reader["ReturnDate"];
                        var borrow = new Borrow() 
                        {
                            ID = (int)reader["ID"],
                            BookID = (int)reader["BookID"],
                            MemberID = (int)reader["MemberID"],
                            BorrowDate = _borrowDate.ToString("yyyy-MM-dd"),
                            ReturnDate = _returnDate.ToString("yyyy-MM-dd"),
                            RemainingDays = RemainDays(_returnDate)
                        };
                        borrows.Add(borrow);    
                    }
                    return borrows;
                }
            }
        }
        public Borrow SelectSpecailBorrowData(int _borrowID) 
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM [Borrowing] WHERE ID = '{_borrowID}'";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    var borrow = new Borrow();

                    while (reader.Read()) 
                    {

                        borrow.BookID = (int)reader["BookID"];
                        borrow.MemberID = (int)reader["MemberID"];
                    }
                    return borrow;
                }
            }
        }
        public int SelectTotalBorrowings() 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"SELECT COUNT(*) FROM[Borrowing]";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
            }
        }
        public int GetBorrowedMembers() 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"SELECT COUNT(DISTINCT MemberID) FROM [Borrowing]";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
            }
        }
        public Borrow SelectEditBorrowDetails(int _borrowID) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"SELECT BookID,MemberID FROM [Borrowing] WHERE ID = '{_borrowID}'";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    Borrow borrow = new Borrow();  
                    while (reader.Read()) 
                    {
                        borrow = new Borrow() 
                        {
                            BookID = (int)reader["BookID"],
                            MemberID = (int)reader["MemberID"]
                        };
                       
                    }
                    return borrow;
                }
            }
        }
        public void UpdateBorrowData(Borrow borrow) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"UPDATE [Borrowing] SET BookID = '{borrow.BookID}', MemberID = '{borrow.MemberID}' WHERE ID = '{borrow.ID}'";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public int GetBookID(int ID) 
        {
            int id = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT BookID FROM [Borrowing] WHERE ID = '{ID}'";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read()) 
                    {
                        id = (int)reader["BookID"];
                    }
                    return id;
                }
            }
        }
    }
}
