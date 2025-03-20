using System;
using System.Data.SqlClient;
using SafeVaultApp.Helpers;

namespace SafeVaultApp.Services
{
    public class UserService
    {
        private readonly string _connectionString = "DefaultConnection";

        public string GetUserEmail(string username)
        {
            if (!ValidationHelpers.IsValidInput(username))
                return "Invalid input";

            string query = "SELECT Email FROM Users WHERE Username = @Username";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    connection.Open();
                    return command.ExecuteScalar()?.ToString() ?? "User not found";
                }
            }
        }
    }
}
