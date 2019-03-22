using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using AuthServer.Entities;
using Dapper;
using Serilog;

namespace AuthServer.Data
{
    public class AuthRepository : IAuthRepository
    {
        protected IConfiguration Configuration { get; }

        protected readonly string connectionString;

        protected IDbConnection db;

        public AuthRepository(IConfiguration configuration)
        {
            Configuration = configuration;

            connectionString = Configuration.GetConnectionString("DefaultConnection");

            db = new NpgsqlConnection(connectionString);
        }

        public User GetUserById(string id)
        {
            Log.Information($"Started getting user by id: {id}.");

            return db.Query<User>("SELECT Id, UserName, Password, IsActive FROM Auth WHERE Id = @id", new { id }).FirstOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            Log.Information($"Started getting user by username: {username}");

            return db.Query<User>("SELECT Id, UserName, Password, IsActive FROM Auth WHERE Username = @username", new { username }).FirstOrDefault();
        }

        public bool ValidatePassword(string username, string plainTextPassword)
        {
            Log.Information("ValidatePassword method called.");

            var user = GetUserByUsername(username);
            if (user == null)
                return false;
            if(String.Equals(plainTextPassword, user.Password))
                return true;
            return false;
        }
    }
}
