using System;
using System.Collections.Generic;

namespace ThreeSeventy.Vector.Client.Tests
{
    class MockConfig : IConfiguration
    {
        public IAuthConfig Authorization
        {
            get { return new MockAuthConfig(); }
            set { }
        }

        public string BaseUrl
        {
            get { return "https://staging-api.3seventy.com/api/main/"; }
            set { }
        }

        public string UserAgent
        {
            get { return "SDK .Net Unit Tester"; } 
            set { }
        }

        public TimeSpan Timeout
        {
            get { return TimeSpan.FromSeconds(30); }
            set { }
        }

        public IRetryConfig RetryPolicy
        {
            get { return new MockRetryConfig(); }
            set { }
        }
    }
}
