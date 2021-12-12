using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoe.Configuration
{
    public class DatabaseConfiguration
    {

        private readonly IConfiguration _configuration;

        public DatabaseConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string MySQLConnectString()
        {
            //var connString = $"server=adverseeventdb;database=adversedb;port=3306;user=root;password=kentish;SslMode=none;";
            var host = _configuration.GetValue<string>("DBHOST") ?? "localhost";
            var port = _configuration.GetValue<string>("DBPORT") ?? "3306";
            var password = _configuration.GetValue<string>("MYSQL_PASSWORD") ?? _configuration.GetConnectionString("MYSQL_PASSWORD");
            var userid = _configuration.GetValue<string>("MYSQL_USER") ?? _configuration.GetConnectionString("MYSQL_USER");
            var usersDataBase = _configuration.GetValue<string>("MYSQL_DATABASE") ?? _configuration.GetConnectionString("MYSQL_DATABASE");
            var connString = $"Server={host};Port={port};Database={usersDataBase};user={userid};password={password};SslMode=none;";

            return connString;
        }
        
    }
}
