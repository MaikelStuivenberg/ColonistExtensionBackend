
using System;
using Dapper;
using colonist_extension.Models.Configuration;
using MySql.Data.MySqlClient;

namespace colonist_extension.Repositories.Database
{
    public class MySQLDatabaseSetup
    {
        public static bool TryCreateDatabase(MySqlSettings mysqlSettings)
        {

            var database = mysqlSettings.Database;

            var builder = new MySqlConnectionStringBuilder
            {
                Server = mysqlSettings.Server,
                UserID = mysqlSettings.UserId,
                Password = mysqlSettings.Password,
                CharacterSet = mysqlSettings.CharacterSet,
            };

            Console.WriteLine("trying to connect to mysql");

            using (var dbCreateConnection = new MySqlConnection(builder.ConnectionString))
            {
                try
                {
                    dbCreateConnection.Open();
                    
                    // Check if the database already exists
                    if (dbCreateConnection.ExecuteScalar<int>("SELECT COUNT(*) FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @Database;", new { Database = database }) == 0)
                    {
                        // Create the database
                        var result = dbCreateConnection.Execute($"CREATE SCHEMA `{database}` DEFAULT CHARACTER SET utf8;");

                        // Database is created
                        if (result == 1){
                            Console.WriteLine("Created");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Database already exists");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Oops..");
                    Console.WriteLine(ex.ToString());
                    // Something happend
                    // TODO Log reason
                    return false;
                }
            }

            return true;
        }
    }
}