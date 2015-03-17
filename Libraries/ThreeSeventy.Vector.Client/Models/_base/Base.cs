using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ThreeSeventy.Vector.Client.Models
{
    /// <summary>
    /// Base for (almost) all model classes.
    /// </summary>
    [Serializable]
    [DataContract]
    public abstract class Base
    {
        /// <summary>
        /// Primary key for the model.
        /// </summary>
        [DataMember]
        public int Id { get; set; }
    }
}
