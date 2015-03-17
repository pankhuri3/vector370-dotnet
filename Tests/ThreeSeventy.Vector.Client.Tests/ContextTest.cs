using System;
using System.Collections.Generic;
using System.Net;

using NUnit.Framework;

using Rhino.Mocks;

using RestSharp;

using ThreeSeventy.Vector.Client.Models;

namespace ThreeSeventy.Vector.Client.Tests
{
    [TestFixture]
    public class ContextTest
    {
        [TestCase]
        public void ContactSearchReturnsResults()
        {
            var mockConfig = new MockConfig();
            var mockFactory = new MockClientFactory();

            var context = new T70Context(mockConfig, mockFactory);

            const string JSON_RESPONSE_DATA = "[]";

            var result = new MockResponse<List<Contact>>
            {
                Content = JSON_RESPONSE_DATA,
                ContentType = "application/json",
                ContentLength = JSON_RESPONSE_DATA.Length,
                ContentEncoding = "utf-8",
                Data = new List<Contact>(),
                StatusCode = HttpStatusCode.OK
            };

            mockFactory.ClientStub.Stub(c => c.Execute(Arg<IRestRequest>.Is.Anything))
                .Return(result)
            ;

            var contacts = context.ContactSearch(25, "512*");

            Assert.IsTrue(mockFactory.WasCalled, "Create() not called on factory!");
            mockFactory.AssertCommonCalls();

            Assert.IsEmpty(contacts);
        }
    }
}
