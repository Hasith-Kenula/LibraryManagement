using database.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace database.Data
{
    public class MemberData
    {
        string connectionString;
        public MemberData()
        {
            ConnectionString connection = new ConnectionString();
            connectionString = connection.Connection;
           

        }
        public bool AddMember(Member _member) 
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                string query = $"INSERT INTO [Member] (Name,Email,Contact_No) VALUES ('{_member.Name}','{_member.Email}','{_member.Contact_No}')";

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                

                return command.ExecuteNonQuery() > 0;

            }
        }
        public List<Member> SelectAllMemberData()
        {

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM [Member]";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dataReader = command.ExecuteReader();

                List<Member> list = new List<Member>();
                while (dataReader.Read())
                {
                    Member member = new Member();

                    member.ID = dataReader.GetInt32(0);
                    member.Name = dataReader.GetString(1);
                    member.Email = dataReader.GetString(2);
                    member.Contact_No = dataReader.GetString(3);


                    list.Add(member);
                }
                return list;

            }

        }
        public Member SelectEditMember(int _ID) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                connection.Open();
                string query = $"SELECT * FROM [Member] WHERE ID = '{_ID}'";
                
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                Member member = new Member();
                if (reader.Read())
                {
                    
                    member.ID = reader.GetInt32(0);
                    member.Name = reader.GetString(1);
                    member.Email = reader.GetString(2);
                    member.Contact_No = reader.GetString(3);

                }

                return member;
            }
        }
        public bool UpdateMember(Member _member , int _id) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                connection.Open();
                string query = $"UPDATE [Member] SET Name = '{_member.Name}' , Email = '{_member.Email}' , Contact_No = '{_member.Contact_No}' WHERE ID = '{_id}'";
                SqlCommand sqlCommand = new SqlCommand( query, connection);

                int rowAffected = sqlCommand.ExecuteNonQuery();

                if (rowAffected > 0)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
  
            }
        }
        public bool DeleteMember(int _ID) 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"DELETE FROM [Member] WHERE ID = '{_ID}'";
                SqlCommand sqlCommand = new SqlCommand(query , connection);
                connection.Open();

                return sqlCommand.ExecuteNonQuery() > 0;
            }
        }
        public List<Member> SearchMember(string _searchText) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"SELECT * FROM [Member] WHERE Name LIKE '%{_searchText}%' OR Email Like '%{_searchText}%'";
                
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    
                    List<Member> memberDetail = new List<Member>();

                    while (reader.Read()) 
                    {
                        var member = new Member();

                        member.ID = Convert.ToInt32(reader["ID"]);
                        member.Name = Convert.ToString(reader["Name"]);
                        member.Email = Convert.ToString(reader["Email"]);
                        member.Contact_No = Convert.ToString(reader["Contact_No"]);

                        memberDetail.Add(member);
                    }
                    return memberDetail;
                }
            }
        }
        public List<Member> SelectSpecialMembers() 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"SELECT ID,Name,Email FROM [Member]";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    List<Member> specialMemberDetail = new List<Member>();

                    while (reader.Read()) 
                    {
                        var member = new Member() 
                        {
                            ID = (int)reader["ID"],
                            Name = (string)reader["Name"],
                            Email = (string)reader["Email"]
                        };
                        specialMemberDetail.Add(member);

                    }
                    return specialMemberDetail;

                }
            }
        }
        public List<Member> SearchSpecialMembers(string _searchText) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"SELECT ID,Name,Email FROM [Member] WHERE ID LIKE '{_searchText}%' OR Name LIKE '%{_searchText}%' OR Email LIKE '%{_searchText}%'";

                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    
                    List<Member> members = new List<Member>();
                    
                    while (reader.Read()) 
                    {
                        var member = new Member()
                        {
                            ID = (int)reader["ID"],
                            Name = (string)reader["Name"],
                            Email = (string)reader["Email"]
                        };
                        members.Add(member);
                    }
                    return members;
                }
            }
        }
        public int GetAllMembers() 
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT COUNT(*) FROM [Member]";
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
            }
        }
        public bool CheckMemberAvailability(int _id) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                string query = $"SELECT COUNT(*) FROM [Member] WHERE ID = '{_id}'";
                
                using (var command = new SqlCommand(query,connection)) 
                {
                    connection.Open();
                    return (int)command.ExecuteScalar() > 0;
                }
            }
        }
    }
}
