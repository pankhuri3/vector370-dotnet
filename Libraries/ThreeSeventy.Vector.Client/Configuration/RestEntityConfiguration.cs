using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ThreeSeventy.Vector.Client.Configuration
{
    /// <summary>
    /// The detailed configuration for an entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity we're a configuration for.</typeparam>
    public class RestEntityConfiguration<TEntity> : RestEntityConfigurationBase
        where TEntity : class, new()
    {
        /// <summary>
        /// List of configured properties.
        /// </summary>
        private readonly IDictionary<string, RestPropertyConfigurationBase> m_props =
            new Dictionary<string, RestPropertyConfigurationBase>();

        /// <summary />
        internal RestEntityConfiguration()
        {
            BuildProperties();
        }

        /// <summary>
        /// Setup the initial list of properties.
        /// </summary>
        private void BuildProperties()
        {
            var props = typeof(TEntity).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var prop in props.Where(p => p.CanRead && p.CanWrite))
            {
                m_props[prop.Name] = new RestPropertyConfiguration<TEntity>(prop);
            }
        }

        /// <summary>
        /// Sets the format string for the URI.
        /// </summary>
        /// <remarks>
        /// URIs contain inserts such as: /account/{accountId}/content/{contentId}
        /// </remarks>
        /// <param name="format">The format string to use.</param>
        /// <param name="kind">The type of URI this is.  (DEFAULT: Relative)</param>
        public void UriFormat(string format, UriKind kind = UriKind.Relative)
        {
            UriFormatStr = format;
            UriKind = kind;
        }

        override internal Type GetBaseType()
        {
            return typeof(TEntity);
        }

        internal override ICollection<RestPropertyConfigurationBase> GetProperties()
        {
            return m_props.Values;
        }

        private PropertyInfo GetPropertyInfo<TProp>(Expression<Func<TEntity, TProp>> property)
        {
            var exp = property.Body as MemberExpression;

            if (exp == null)
                throw new ArgumentException("The lambda expression 'property' does not point to a valid property");

            var rval = exp.Member as PropertyInfo;

            if (rval == null)
                throw new ArgumentException("The lambda expression 'property' should point to a valid property");

            return rval;
        }

        /// <summary>
        /// Gets the RestPropertyConfiguration object for the supplied property.
        /// </summary>
        /// <typeparam name="TProp">The type of the property we're configuring for.</typeparam>
        /// <param name="property">A lambda expression that specifies the property to configure.</param>
        public RestPropertyConfiguration<TEntity> Property<TProp>(Expression<Func<TEntity, TProp>> property)
        {
            PropertyInfo info = GetPropertyInfo(property);

            if (!m_props.ContainsKey(info.Name))
                m_props[info.Name] = new RestPropertyConfiguration<TEntity>(info);

            return m_props[info.Name] as RestPropertyConfiguration<TEntity>;
        }

        internal override IEnumerable<RestPropertyConfigurationBase> GetPrimaryKeys()
        {
            return m_props.Values.Where(p => p.IsPrimaryKey);
        }

        internal override void ValidateHasNeededKeys(object args)
        {
            if (args == null)
                throw new ArgumentNullException("args");

            Type t = args.GetType();

            foreach (var kvp in m_props)
            {
                if (kvp.Value.UrlMapType != MappingType.UrlSegment)
                    continue;

                string propName = kvp.Key;

                var prop = t.GetProperty(propName, BindingFlags.Instance | BindingFlags.Public);

                if ((prop == null) || !prop.CanRead)
                    throw new Exception("Missing required key " + propName);
            }
        }
    }
}
