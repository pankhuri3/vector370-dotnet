using System;
using System.Collections.Generic;

using RestSharp;

using Rhino.Mocks;

namespace ThreeSeventy.Vector.Client.Tests
{
    class MockClientFactory : IRestClientFactory
    {
        public MockClientFactory()
        {
            ClientStub = MockRepository.GenerateStub<IRestClient>();
        }
        
        public bool WasCalled { get; private set; }

        public IRestClient ClientStub { get; private set; }

        // Verifies that all commonly expected calls have been made
        public void AssertCommonCalls()
        {
            //ClientStub.AssertWasCalled(c => c.Authenticator = new HttpBasicAuthenticator("test", "test"));
        }

        public IRestClient Create(IConfiguration config)
        {
            WasCalled = true;

            return ClientStub;
        }
    }
}
