using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApi;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;

namespace LibraryApi.Test.TestUtilities
{
    public class CustomApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        /*HttpClient TestClient = null;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder 
            //var serviceProvider = new ServiceCollection
        }

        // Creating a client
        public HttpClient GetTestClient()
        {

        }*/
    }
}
