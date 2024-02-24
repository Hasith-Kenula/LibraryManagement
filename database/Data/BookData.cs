using database.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace database.Data
{
    public class BookData
    {
        string connectionString;
        public BookData()
        {
            ConnectionString connection = new ConnectionString();
            connectionString = connection.Connection;
        }
        public List<Book> selectAllBookData() 
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                string query = $"SELECT * FROM [Book]";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();
                
                List<Book> bookList = new List<Book>();

                while (reader.Read()) 
                {
                    Book book = new Book();
                    book.ID = reader.GetInt32(0);
                    book.Title = reader.GetString(1);
                    book.Publisher = reader.GetString(2);
                    book.Author = reader.GetString(3);
                    book.ISBN = reader.GetString(4);
                    book.Category = reader.GetString(5);
                    book.QTY = reader.GetInt32(6);

                    bookList.Add(book);
                }
                return bookList;
            }
        }
        public Book SelectEditBook(int _ID) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"SELECT * FROM [Book] WHERE ID = '{_ID}'";
                var sqlcommand = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = sqlcommand.ExecuteReader();

                Book book = new Book();

                while (reader.Read()) 
                {
                    book.Title = reader["Title"].ToString();
                    book.Publisher = reader["Publisher"].ToString();
                    book.Author = reader["Author"].ToString();
                    book.ISBN = reader["ISBN"].ToString();
                    book.Category = reader["Category"].ToString();
                    book.QTY = (int)reader["QTY"];
                }

                return book;
            }
        }
        public bool UpdateBookData(Book _book) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"UPDATE [Book] SET Title = '{_book.Title}', Publisher = '{_book.Publisher}', Author = '{_book.Author}', ISBN = '{_book.ISBN}', Category = '{_book.Category}', QTY = '{_book.QTY}' WHERE ID = '{_book.ID}'";

                using (SqlCommand updateCommand = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    return updateCommand.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool AddBookData(Book _book)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"INSERT INTO [Book](Title,Publisher,Author,ISBN,Category,QTY) VALUES ('{_book.Title}','{_book.Publisher}','{_book.Author}','{_book.ISBN}','{_book.Category}', '{_book.QTY}')";

                using (SqlCommand command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        public List<Book> SearchBooks(string _searchText) 
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM [Book] WHERE Title LIKE '%{_searchText}%' OR Publisher LIKE '%{_searchText}%' OR Author LIKE '%{_searchText}%' OR Category LIKE '%{_searchText}%'";
                
                using (SqlCommand command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    
                    SqlDataReader reader = command.ExecuteReader();

                    List<Book> books = new List<Book>();
                    while (reader.Read()) 
                    {
                        Book book = new Book()
                        {
                            ID = (int)reader["ID"],
                            Title = (string)reader["Title"],
                            Publisher = (string)reader["Publisher"],
                            Author = (string)reader["Author"],
                            ISBN = (string)reader["ISBN"],
                            Category = (string)reader["Category"],
                            QTY = (int)reader["QTY"]

                        };
                        books.Add(book);
                    }
                    return books;
                }
            }
        }
        public List<Book> SelectBooksSpecialData() 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = "SELECT ID,Title,Author,Category,QTY FROM [Book]";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    List<Book> books = new List<Book>();

                    while (reader.Read()) 
                    {
                        Book book = new Book()
                        {
                            ID = (int)reader["ID"],
                            Title = (string)reader["Title"],
                            Author = (string)reader["Author"],
                            Category = (string)reader["Category"],
                            QTY = (int)reader["QTY"]
                        };
                        books.Add(book);
                    }
                    return books;
                }
            }
        }
        public List <Book> SearchBooksSpecialData(string _searchText) 
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT ID,Title,Author,Category,QTY FROM [Book] WHERE ID LIKE '{_searchText}%' OR Title LIKE '%{_searchText}%' OR Author LIKE '%{_searchText}%' OR Category LIKE '%{_searchText}%'";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    List<Book> books = new List<Book>();

                    while (reader.Read()) 
                    {
                        Book book = new Book() 
                        {
                            ID = (int)reader["ID"],
                            Title = (string)reader["Title"],
                            Author = (string)reader["Author"],
                            Category = (string)reader["Category"],
                            QTY = (int)reader["QTY"]
                        };
                        books.Add(book);
                    }
                    return books;
                }

            }
        }
        public int CheckBookStock(int _ID)
        {
            int qty = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT QTY FROM [Book] WHERE ID = '{_ID}'";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        qty = (int)reader["QTY"];
                    }
                    return qty;
                }
            }
        }
        public void UpdateBookStockAfterReserveBook(int _bookID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"UPDATE [Book] SET QTY = QTY-1 WHERE ID = '{_bookID}'";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateBookStockAfterReturnBook(int _bookID) 
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"UPDATE [Book] SET QTY = QTY+1 WHERE ID = {_bookID}";
                using (var command = new SqlCommand(query,connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public int SelectAvailableBookCount() 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"SELECT SUM(QTY) FROM [Book]";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
            }
        }
        public bool UpdateBookQTY(int _ID) 
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"UPDATE [Book] SET QTY=QTY+1 WHERE ID = '{_ID}'";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
