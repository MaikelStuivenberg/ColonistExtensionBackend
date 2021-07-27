using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace colonist_extension.Repositories.Database
{
    public interface IDatabaseConnection
    {
        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        void Query(string query, Action afterQuery = null);
        Task QueryAsync(string query, Action afterQuery = null);
        
        /// <summary>
        /// Queries the specified query with parameters
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        void Query(string query, IDictionary<string, object> parameters, Action afterQuery = null);
        Task QueryAsync(string query, IDictionary<string, object> paramaters, Action afterQuery = null);
        
        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string query, Action<IEnumerable<T>> afterQuery = null);
        Task<IEnumerable<T>> QueryAsync<T>(string query, Action<IEnumerable<T>> afterQuery = null);

        /// <summary>
        /// Queries the specified query with parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string query, IDictionary<string, object> parameters, Action<IEnumerable<T>> afterQuery = null);
        Task<IEnumerable<T>> QueryAsync<T>(string query, IDictionary<string, object> parameters, Action<IEnumerable<T>> afterQuery = null);

        /// <summary>
        /// Executes a query that returns a scalar value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        T QueryScalar<T>(string query, IDictionary<string, object> parameters, Action<T> afterQuery = null);
        Task<T> QueryScalarAsync<T>(string query, IDictionary<string, object> parameters, Action<T> afterQuery = null);

        /// <summary>
        /// Executes a query that returns the amount of effected rows
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        int Execute(string query, Action afterQuery = null);
        Task<int> ExecuteAsync(string query, Action afterQuery = null);
        
        /// <summary>
        /// Executes a query that returns the amount of effected rows
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="afterQuery">The after query.</param>
        /// <returns></returns>
        int Execute(string query, IDictionary<string, object> parameters, Action afterQuery = null);
        Task<int> ExecuteAsync(string query, IDictionary<string, object> parameters, Action afterQuery = null);
        Task<int> ExecuteAsync(string query, IEnumerable<IDictionary<string, object>> parametersArray, Action afterQuery = null);
        
        
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <returns></returns>
        string GetConnectionString();

    }
}