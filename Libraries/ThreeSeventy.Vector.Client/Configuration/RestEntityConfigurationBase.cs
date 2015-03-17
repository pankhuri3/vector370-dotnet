using System;
using System.Collections.Generic;

namespace ThreeSeventy.Vector.Client.Configuration
{
    /// <summary>
    /// Abstract class for the RestEntityConfiguration.
    /// </summary>
    /// <remarks>
    /// This class provides the generic interface to make keeping a list of these easier.
    /// 
    /// We don't use an interface so we can keep the guts internal to this assembly, but still provide the needed 
    /// generic interface for the outside world.
    /// </remarks>
    public abstract class RestEntityConfigurationBase
    {
        /// <summary>
        /// Gets or sets the URI format string.
        /// </summary>
        /// <remarks>
        /// This string contains special inserts for the required and optional URI pramaters.
        /// </remarks>
        /// <example>
        /// /account/{accountId}/contact/{contactId}
        /// </example>
        internal String UriFormatStr { get; set; }

        /// <summary>
        /// Gets or sets the URI kind.
        /// </summary>
        /// <remarks>
        /// This is used to allow an entity to map to a completely different URI by ignoring the base URI specified in 
        /// the context.
        /// </remarks>
        internal UriKind UriKind { get; set; }

        /// <summary>
        /// Returns the base type this configuration is for.
        /// </summary>
        internal abstract Type GetBaseType();

        /// <summary>
        /// Gets a list of properties that have been configured for this entity.
        /// </summary>
        internal abstract ICollection<RestPropertyConfigurationBase> GetProperties();

        /// <summary>
        /// Gets a list of properties that have been marked as being the primary keys for this entity.
        /// </summary>
        /// <remarks>
        /// There needs to be at least one property marked as the primary key, but multiple keys are 
        /// allowed.
        /// </remarks>
        internal abstract IEnumerable<RestPropertyConfigurationBase> GetPrimaryKeys();

        /// <summary>
        /// Validates that the supplied object "args" contains the needed keys to perform an operation.
        /// </summary>
        /// <remarks>
        /// If the object does not contain a needed key then an exception is thrown.
        /// </remarks>
        /// <param name="args">The object containing the keys to check.</param>
        internal abstract void ValidateHasNeededKeys(object args);
    }

}
