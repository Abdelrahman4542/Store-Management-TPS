using Microsoft.Data.SqlClient;
using StoreManagementSystem.Helpers;
using StoreManagementSystem.Models;

namespace StoreManagementSystem.Repositories
{
    public class UserRepository
    {
        // ================= LOGIN =================

        public async Task<User?> LoginAsync(
            string username,
            string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            using SqlConnection connection =
                DbConnection.GetConnection();

            await connection.OpenAsync();

            string query =
                @"SELECT *
                  FROM Users
                  WHERE Username = @Username
                  AND PasswordHash = @Password
                  AND IsActive = 1";

            using SqlCommand command =
                new SqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@Username",
                username.Trim());

            command.Parameters.AddWithValue(
                "@Password",
                password);

            using SqlDataReader reader =
                await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
            {
                return null;
            }

            return new User
            {
                UserId =
                    (int)reader["UserId"],

                Username =
                    reader["Username"].ToString()!,

                PasswordHash =
                    reader["PasswordHash"].ToString()!,

                FullName =
                    reader["FullName"].ToString()!,

                Role =
                    reader["Role"].ToString()!,

                IsActive =
                    (bool)reader["IsActive"]
            };
        }
    }
}