using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

using RestSharp;

using ThreeSeventy.Vector.Client.Configuration;
using ThreeSeventy.Vector.Client.Utils;

namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// Provides a repository interface for a given entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity this repository is for.</typeparam>
    public class RestRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        private readonly IRetryPolicy m_retryPolicy;

        private readonly IRestClient m_client;

        private readonly string m_baseUri;

        private readonly object m_requiredArgs;

        private readonly RestEntityConfiguration<TEntity> m_config;

        internal RestRepository(
            IRetryPolicy retryPolicy,
            IRestClient client,
            string baseUri,
            object requiredArgs,
            RestEntityConfiguration<TEntity> config)
        {
            m_retryPolicy = retryPolicy;
            m_client = client;
            m_baseUri = baseUri;
            m_requiredArgs = requiredArgs;
            m_config = config;
        }

        private IRestResponse ExecuteRequestFor(Method method, object requestBody, object extraArgs, bool nullOn404)
        {
            var builder = new RequestBuilder(m_config);
            var request = builder.BuildRequest(m_baseUri, method, requestBody, m_requiredArgs, extraArgs);

            return m_client.ExecuteRequestFor(m_retryPolicy, request, nullOn404);
        }

        /// <summary>
        /// Gets a specific entity from REST.
        /// </summary>
        /// <remarks>
        /// For objects with a compound key 'id' can be a complex object who's properties are 
        /// each part of the compound key.
        /// 
        /// For example:
        /// var sub = contactSubRepo.Get(new { ContactId = 5, SubscriptionId = 10 });
        /// 
        /// If there's a single key, there is no need for boxing:
        /// var contact = contactRepo.Get(5);
        /// </remarks>
        /// <typeparam name="TKey">The type of object that specifies the key data.</typeparam>
        /// <param name="id">The ID of the object being pulled</param>
        /// <param name="throwIfNotFound">Set to true if an error should be thrown if an object is not found.
        /// If this is false, then NULL is returned when an object is not found.</param>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <returns>The object if found, or NULL if not.
        /// If throwIfNotFound is set to true then an Exception will be thrown when the object cannot be located.
        /// </returns>
        public TEntity Get<TKey>(TKey id, bool throwIfNotFound = false)
        {
            var keys = m_config.GetPrimaryKeys().ToList();
            int count = keys.Count();

            var extraArgs = new ExpandoObject();
            var extraArgsDict = (IDictionary<string, object>)extraArgs;

            if (count == 1)
            {
                var k = keys.First();
                extraArgsDict[k.Name] = id;
            }
            else
            {
                var props = ObjectHelper.ToDictionary(id);

                foreach (var k in keys)
                    extraArgsDict[k.Name] = props[k.Name];
            }

            var response = ExecuteRequestFor(Method.GET, null, extraArgs, !throwIfNotFound);

            return (response != null) ? response.Deserialize<TEntity>() : null;
        }

        /// <summary>
        /// Returns a list of all objects availble from REST
        /// </summary>
        /// <remarks>
        /// In the future this will apply an oData query to the results via the IQueryable interface.
        /// 
        /// For now however, be aware that this will immediately query for all of the data available on the server and
        /// perform the query using LINQ to objects.
        /// </remarks>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <returns>An IQueryable for the given entity type.</returns>
        public IQueryable<TEntity> GetAll()
        {
            var response = ExecuteRequestFor(Method.GET, null, null, false);

            var list = response.Deserialize<List<TEntity>>() ?? new List<TEntity>();

            return list.AsQueryable();
        }

        /// <summary>
        /// Adds an object via REST.
        /// </summary>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <param name="entity">The entity to add</param>
        public void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var response = ExecuteRequestFor(Method.POST, entity, null, false);

            var result = response.Deserialize<TEntity>();

            var keys = m_config.GetPrimaryKeys();

            foreach (var key in keys)
            {
                object value = key.PropertyInfo.GetValue(result);

                key.PropertyInfo.SetValue(entity, value);
            }
        }

        /// <summary>
        /// Adds a list of objects via REST
        /// </summary>
        /// <remarks>
        /// At the moment this is just a convenience wrapper around Add().
        /// </remarks>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <param name="entities">The list of entites to add</param>
        public void AddRange(ICollection<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var e in entities)
                Add(e);
        }

        /// <summary>
        /// Updates an object via REST
        /// </summary>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <param name="entity">The object to update</param>
        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var response = ExecuteRequestFor(Method.PUT, entity, entity, false);

            var result = response.Deserialize<TEntity>();

            var keys = m_config.GetPrimaryKeys();

            foreach (var key in keys)
            {
                object value = key.PropertyInfo.GetValue(result);

                key.PropertyInfo.SetValue(entity, value);
            }
        }

        /// <summary>
        /// Updates a list of objects via REST
        /// </summary>
        /// <remarks>
        /// This is a convenience wrapper for calling Update()
        /// </remarks>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <param name="entities">The list of objects to add</param>
        public void UpdateRange(ICollection<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var e in entities)
                Update(e);
        }

        /// <summary>
        /// Removes an object from REST
        /// </summary>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <param name="entity">The object ot delete</param>
        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            ExecuteRequestFor(Method.DELETE, null, entity, true);
        }

        /// <summary>
        /// Removes a list of objects from REST
        /// </summary>
        /// <remarks>
        /// This is a convenience wrapper for calling Delete()
        /// </remarks>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <param name="entries">The list of objects to remove.</param>
        public void DeleteAll(ICollection<TEntity> entries)
        {
            if (entries == null)
                throw new ArgumentNullException("entities");

            foreach (var e in entries)
                Delete(e);
        }
    }
}
