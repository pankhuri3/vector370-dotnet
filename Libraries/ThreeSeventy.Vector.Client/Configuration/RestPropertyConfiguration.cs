using System;
using System.Collections.Generic;
using System.Reflection;

namespace ThreeSeventy.Vector.Client.Configuration
{
    /// <summary>
    /// Configuration details for an entity's property.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that is being configured.</typeparam>
    public class RestPropertyConfiguration<TEntity> : RestPropertyConfigurationBase
        where TEntity : class, new()
    {
        private readonly PropertyInfo m_propInfo;

        private readonly string m_name;

        private string m_mappingName;

        private MappingType m_urlMapType = MappingType.Post;

        private bool m_isPrimaryKey;

        internal RestPropertyConfiguration(PropertyInfo propInfo)
        {
            m_propInfo = propInfo;
            m_name = propInfo.Name;
        }

        internal override PropertyInfo PropertyInfo
        {
            get { return m_propInfo; }
        }

        internal override string Name
        {
            get { return m_name; }
        }

        internal override string MappingName
        {
            get { return m_mappingName ?? Name; }
        }

        internal override MappingType UrlMapType { get { return m_urlMapType; } }

        internal override bool IsPrimaryKey { get { return m_isPrimaryKey; } }

        /// <summary>
        /// Sets this property as a primary key for the entity.
        /// </summary>
        public RestPropertyConfiguration<TEntity> PrimaryKey()
        {
            m_isPrimaryKey = true;
            return this;
        }

        /// <summary>
        /// Sets this property as a required URL segment parameter.
        /// </summary>
        /// <remarks>
        /// URL segment parameters are those parts of the URL which are not supplied as a GET parameter.
        /// </remarks>
        public RestPropertyConfiguration<TEntity> UrlSegment()
        {
            m_urlMapType = MappingType.UrlSegment;
            return this;
        }

        /// <summary>
        /// Sets this property as an optional URL segment parameter.
        /// </summary>
        /// <remarks>
        /// This functions the same as UrlSegment() except that the parameter is not required.
        /// If this parameter is missing then that URL segment will be left blank.
        /// </remarks>
        /// <returns></returns>
        public RestPropertyConfiguration<TEntity> OptionalUrlSegment()
        {
            m_urlMapType = MappingType.OptionalUrlSegment;
            return this;
        }

        /// <summary>
        /// Sets this property as a GET parameter.
        /// </summary>
        public RestPropertyConfiguration<TEntity> Get()
        {
            m_urlMapType = MappingType.Get;
            return this;
        }

        /// <summary>
        /// Sets this property as a POST parameter.
        /// </summary>
        /// <returns></returns>
        public RestPropertyConfiguration<TEntity> Post()
        {
            m_urlMapType = MappingType.Post;
            return this;
        }

        /// <summary>
        /// Sets this property as being either GET or POST.
        /// </summary>
        /// <remarks>
        /// For GET and DELETE operations this parameter will be supplied as a GET parameter.
        /// For PUT and POST this parameter will be supplied in the HTTP body.
        /// </remarks>
        public RestPropertyConfiguration<TEntity> GetOrPost()
        {
            m_urlMapType = MappingType.GetOrPost;
            return this;
        }

        /// <summary>
        /// Sets the property as being set via an HTTP cookie.
        /// </summary>
        /// <returns></returns>
        public RestPropertyConfiguration<TEntity> Cookie()
        {
            m_urlMapType = MappingType.Cookie;
            return this;
        }

        /// <summary>
        /// Sets the property as being set via an HTTP header.
        /// </summary>
        /// <returns></returns>
        public RestPropertyConfiguration<TEntity> Header()
        {
            m_urlMapType = MappingType.Header;
            return this;
        }

        /// <summary>
        /// Sets how this property should be named when (de)serializing from the HTTP stream.
        /// </summary>
        /// <param name="name">The name to use in the HTTP streams.</param>
        public RestPropertyConfiguration<TEntity> MapTo(string name)
        {
            m_mappingName = name;
            return this;
        }
    }
}
