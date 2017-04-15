using System;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApiApplication;

namespace UnitTest
{
    public class BaseTestFixture: IDisposable
    {
        public readonly TestServer Server;
        
        public readonly HttpClient Client;

        public readonly DataContext TestContext;   

        public readonly IConfigurationRoot Configuration;

        public BaseTestFixture()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional:true, reloadChange: true)
                .AddJsonFile($"appsettings.{envName}.json", optional:true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var opts = new DbContextOptionsBuilder<DataContext>();    
            opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            TestContext = new DataContext(opts.Options);
            SetupDataBase();
            Server = new TestServer(new WebHostBuilder().UserStartup<Startup>());
            Client = Server.CreateClient();
        }

        private void SetupDataBase()
        {
            try
            {
                TestContext.DataBase.EnsureCreated();
                TestContext.DataBase.Migrate();
            }
            catch (System.Exception)
            {                
                throw;
            }
        }    

        public void Dispose
        {
            TestContext.Dispose;
            Server.Dispose;
            Client.Dispose;
        } 
    }
}