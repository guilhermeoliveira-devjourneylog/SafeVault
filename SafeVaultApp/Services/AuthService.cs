using SafeVaultApp.Models;
using MySqlConnector;

namespace SafeVaultApp.Services
{
    public class AuthService
    {
        private readonly string _connectionString;

        public AuthService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void RegisterUser(string username, string email, string password, string role)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password); 

            using (var connection = new MySqlConnection(_connectionString)) 
            {
                string query = "INSERT INTO Users (Username, Email, PasswordHash, Role) VALUES (@Username, @Email, @PasswordHash, @Role)";

                using (var command = new MySqlCommand(query, connection)) 
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    command.Parameters.AddWithValue("@Role", role);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public User? ValidateUser(string username, string password)
        {
            using (var connection = new MySqlConnection(_connectionString)) 
            {
                string query = "SELECT UserID, Username, Email, PasswordHash, Role FROM Users WHERE Username = @Username";

                using (var command = new MySqlCommand(query, connection)) 
                {
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["PasswordHash"].ToString();
                            if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                            {
                                return new User
                                {
                                    UserID = Convert.ToInt32(reader["UserID"]),
                                    Username = reader["Username"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Role = reader["Role"].ToString() 
                                };
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
