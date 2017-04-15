using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using WebApiApplication;

namespace UnitTest
{
    public abstract class BaseIntegrationTest
    {
        protected readonly TestServer Server;
        
        protected readonly HttpClient Client;

        protected readonly DataContext Context;

        protected BaseTestFixture Fixture {get;}   

        public void BaseIntegrationTest(BaseTestFixture fixture)
        {
            Fixture = fixture;
            Server  = fixture.Server;
            Client  = fixture.Client;
        }     
    }
}