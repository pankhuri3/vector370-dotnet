using System;
using System.Collections.Generic;
using System.Configuration;

namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// App.Config interface for IAuthConfig
    /// </summary>
    public class VectorAuthElement : ConfigurationElement, IAuthConfig
    {
        /// <summary>
        /// The user name that should be sent.
        /// </summary>
        [ConfigurationProperty("username", IsRequired = true)]
        public string UserName
        {
            get { return (string)this["username"]; }
            set { this["username"] = value; }
        }

        /// <summary>
        /// The password that should be sent.
        /// </summary>
        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }
    }
}
