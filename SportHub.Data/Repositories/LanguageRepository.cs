﻿using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SportHub.Data.Entities;
using SportHub.Data.Interfaces;

namespace SportHub.Data.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public LanguageRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;

        }

        public async Task DeleteLanguageAsync(string shortTitle)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                var sql = $"DELETE FROM language WHERE ShortTitle='{shortTitle}';";
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task AddLanguageAsync(Language language)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                var sql = "INSERT INTO Language (ShortTitle, IsActive) " +
                    "VALUES (@ShortTitle, @IsActive);";
                await connection.ExecuteAsync(sql, language);
            }
        }

        public async Task ChangeLanguageIsActiveAsync(string shortTitle, bool isActive)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                var sql = $"UPDATE Language SET IsActive = {isActive} WHERE ShortTitle = '{shortTitle}';";
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task<Language> GetLanguageByTitleAsync(string shortTitle)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                var sql = $"SELECT * FROM language WHERE ShortTitle='{shortTitle}';";
                var response = await connection.QueryFirstOrDefaultAsync<Language>(sql);

                return response;
            }
        }

        public async Task<IEnumerable<Language>> GetLanguagesAsync()
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                var sql = "SELECT * FROM language";
                var response = await connection.QueryAsync<Language>(sql);

                return response;
            }
        }
    }
}