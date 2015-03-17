using System;
using System.Collections.Generic;

namespace ThreeSeventy.Vector.Client.Tests
{
    class MockAuthConfig : IAuthConfig
    {
        public string UserName
        {
            get { return "sdkunittest"; }
            set { }
        }

        public string Password
        {
            get { return "nob4dtest5!"; }
            set { }
        }
    }
}
