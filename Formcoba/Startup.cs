using Microsoft.AspNetCore.Builder;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Formcoba
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //...
            app.UseStaticFiles();
            //...
        }

    }
}
