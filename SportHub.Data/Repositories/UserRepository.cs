using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SportHub.Data.Entities;
using SportHub.Data.Interfaces;

namespace SportHub.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
    
        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
            
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                var response = await connection.QueryFirstOrDefaultAsync<User>($"SELECT * FROM user WHERE Email = '{email}';");

                return response;
            }
        }
        
        public async Task<User> GetUserByIdAsync(string id)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                var response = await connection.QueryFirstOrDefaultAsync<User>($"SELECT * FROM user WHERE Id = '{id}';");

                return response;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                var sql = "SELECT * FROM user";
                var users = await connection.QueryAsync<User>(sql);
                
                return users;
            }  
        }

        public async Task InsertOneAsync(User user)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                var sql = "INSERT INTO User (Id, IsActivated, IsAdmin, Password, Email, FirstName, LastName) " +
                          "VALUES (@Id, @IsActivated, @IsAdmin, @Password, @Email, @FirstName, @LastName)";
                await connection.ExecuteAsync(sql, user);
            }
        }
    }
}

