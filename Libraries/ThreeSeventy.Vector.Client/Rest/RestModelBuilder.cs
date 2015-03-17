using System;
using System.Collections.Generic;

using ThreeSeventy.Vector.Client.Configuration;

namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// The model building configuration class
    /// </summary>
    /// <remarks>
    /// This holds a collection of RestEntityConfigurationBase objects.
    /// </remarks>
    public sealed class RestModelBuilder
    {
        private readonly IDictionary<Type, RestEntityConfigurationBase> m_configs =
            new Dictionary<Type, RestEntityConfigurationBase>();

        /// <summary>
        /// Gets the configuration object for a type of entity.
        /// </summary>
        /// <typeparam name="TEntity">The type that is being configured.</typeparam>
        /// <returns>A RestEntityConfiguration object.</returns>
        public RestEntityConfiguration<TEntity> Entity<TEntity>()
            where TEntity : class, new()
        {
            Type t = typeof(TEntity);

            if (!m_configs.ContainsKey(t))
                m_configs[t] = new RestEntityConfiguration<TEntity>();

            return m_configs[t] as RestEntityConfiguration<TEntity>;
        }

        internal RestEntityConfiguration<TEntity> GetConfig<TEntity>()
            where TEntity : class, new()
        {
            var rval = m_configs[typeof(TEntity)] as RestEntityConfiguration<TEntity>;

            if (rval == null)
                throw new KeyNotFoundException();

            return rval;
        }

        private void CheckUriProperties(RestEntityConfigurationBase config)
        {
            var props = config.GetProperties();
            bool hasPrimaryKey = false;

            var mappingNames = new HashSet<string>();

            foreach (var p in props)
            {
                hasPrimaryKey |= p.IsPrimaryKey;

                // TODO: This check should be in the RestEntityConfiguration
                if (mappingNames.Contains(p.MappingName))
                {
                    string msg = String.Format(
                        "The mapping '{0}' has been mapped to more than one property.",
                        p.MappingName);

                    throw new Exception(msg);
                }

                mappingNames.Add(p.MappingName);
            }

            if (!hasPrimaryKey)
            {
                string msg = String.Format("No primary key(s) defined for type {0}", config.GetBaseType().FullName);

                throw new Exception(msg);
            }
        }

        internal void CheckEntries()
        {
            foreach (var kvp in m_configs)
            {
                CheckUriProperties(kvp.Value);
            }
        }
    }
}
