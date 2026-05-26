using Microsoft.Data.SqlClient;
using StoreManagementSystem.Helpers;
using StoreManagementSystem.Models;

namespace StoreManagementSystem.Services
{
    public class AuthService
    {
        public bool TestConnection()
        {
            try
            {
                using SqlConnection connection = DbConnection.GetConnection();

            connection.Open();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public User? Login(string username, string password)
        {
            using SqlConnection connection = DbConnection.GetConnection();

            string query = @"
            SELECT *
            FROM Users
            WHERE Username = @Username
            AND PasswordHash = @Password";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    UserId = (int)reader["UserId"],
                    Username = reader["Username"].ToString(),
                    FullName = reader["FullName"].ToString(),
                    PasswordHash = reader["PasswordHash"].ToString(),
                    Role = reader["Role"].ToString(),
                    IsActive = (bool)reader["IsActive"]
                };
            }

            return null;
        }
    }

}
