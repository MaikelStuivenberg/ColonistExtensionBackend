using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using colonist_extension.Models.Configuration;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace colonist_extension.Repositories.Database
{
    public class MySQLConnection : IDatabaseConnection
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of <see cref="MySQLConnection"/>
        /// </summary>
        /// <param name="config"></param>
        public MySQLConnection(IOptionsMonitor<MySqlSettings> settingsSnapshot): this(settingsSnapshot.CurrentValue)
        {
        }

        public MySQLConnection(MySqlSettings settings)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = settings.Server,
                Database = settings.Database,
                UserID = settings.UserId,
                Password = settings.Password,
                CharacterSet = settings.CharacterSet,
            };

            _connectionString = builder.ConnectionString;
        }

        private IDbConnection EnsureOpen(IDbConnection conn)
        {
            if (conn == null) throw new ArgumentNullException(nameof(conn));

            if (conn.State != ConnectionState.Open)
            {
                conn.Open(); // Let exceptions bubble up
            }

            return conn;
        }

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        public void Query(string query, Action afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    connection.Query(query).ToList();
                    afterQuery?.Invoke();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        public async Task QueryAsync(string query, Action afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    (await connection.QueryAsync(query)).ToList();
                    afterQuery?.Invoke();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Queries the specified query with parameters
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        public void Query(string query, IDictionary<string, object> parameters, Action afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    connection.Query(query, parameters).ToList();
                    afterQuery?.Invoke();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Queries the specified query with parameters
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        public async Task QueryAsync(string query, IDictionary<string, object> parameters, Action afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    (await connection.QueryAsync(query, parameters)).ToList();
                    afterQuery?.Invoke();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string query, Action<IEnumerable<T>> afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var result = connection.Query<T>(query).ToList();
                    afterQuery?.Invoke(result);
                    tran.Commit();
                    return result;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string query, Action<IEnumerable<T>> afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var result = (await connection.QueryAsync<T>(query)).ToList();
                    afterQuery?.Invoke(result);
                    tran.Commit();
                    return result;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Queries the specified query with parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string query, IDictionary<string, object> parameters, Action<IEnumerable<T>> afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var result = connection.Query<T>(query, parameters).ToList();
                    afterQuery?.Invoke(result);
                    tran.Commit();
                    return result;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Queries the specified query with parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string query, IDictionary<string, object> parameters, Action<IEnumerable<T>> afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var result = (await connection.QueryAsync<T>(query, parameters)).ToList();
                    afterQuery?.Invoke(result);
                    tran.Commit();
                    return result;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Executes a query that returns a scalar value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        public T QueryScalar<T>(string query, IDictionary<string, object> parameters, Action<T> afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var result = connection.ExecuteScalar<T>(query, parameters);
                    afterQuery?.Invoke(result);
                    tran.Commit();
                    return result;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Executes a query that returns a scalar value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        public async Task<T> QueryScalarAsync<T>(string query, IDictionary<string, object> parameters, Action<T> afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var result = (await connection.ExecuteScalarAsync<T>(query, parameters));
                    afterQuery?.Invoke(result);
                    tran.Commit();
                    return result;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public int Execute(string query, Action afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var result = connection.Execute(query);
                    afterQuery?.Invoke();
                    tran.Commit();

                    return result;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public async Task<int> ExecuteAsync(string query, Action afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var result = await connection.ExecuteAsync(query);
                    afterQuery?.Invoke();
                    tran.Commit();

                    return result;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public int Execute(string query, IDictionary<string, object> parameters, Action afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var result = connection.Execute(query, parameters);
                    afterQuery?.Invoke();
                    tran.Commit();

                    return result;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public async Task<int> ExecuteAsync(string query, IDictionary<string, object> parameters, Action afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var result = await connection.ExecuteAsync(query, parameters);
                    afterQuery?.Invoke();
                    tran.Commit();

                    return result;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public async Task<int> ExecuteAsync(string query, IEnumerable<IDictionary<string, object>> parametersArray, Action afterQuery = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            using (var tran = EnsureOpen(connection).BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var result = await connection.ExecuteAsync(query, parametersArray);
                    afterQuery?.Invoke();
                    tran.Commit();

                    return result;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}