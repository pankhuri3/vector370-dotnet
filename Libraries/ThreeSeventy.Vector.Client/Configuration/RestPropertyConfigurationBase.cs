using System;
using System.Collections.Generic;
using System.Reflection;

namespace ThreeSeventy.Vector.Client.Configuration
{
    /// <summary>
    /// Configuration details for an entity's property.
    /// </summary>
    public abstract class RestPropertyConfigurationBase
    {
        internal abstract PropertyInfo PropertyInfo { get; }

        internal abstract string Name { get; }

        internal abstract string MappingName { get; }

        internal abstract MappingType UrlMapType { get; }

        internal abstract bool IsPrimaryKey { get; }
    }
}