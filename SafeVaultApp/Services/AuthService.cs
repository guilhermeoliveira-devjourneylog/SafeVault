using System;
using System.Data.SqlClient;
using SafeVaultApp.Helpers;

namespace SafeVaultApp.Services
{
    public class AuthService
    {
        private readonly string _connectionString;

        public AuthService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool LoginUser(string username, string password)
        {
            string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND Password = @Password";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password); // ⚠️ Use bcrypt for password hashing!

                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
