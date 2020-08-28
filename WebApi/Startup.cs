using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Microsoft.AspNetCore.Hosting;
using WebApi.Models;
using Microsoft.AspNetCore.Routing;


//register the database context ( data context to Dependency Injection container)
namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddControllers();

            services.AddDbContext<ApplicationContext>(opt =>
            {
                opt.UseNpgsql("Host=127.0.0.1;Port=5432;User Id=postgres;Password=snh86mmocc10;Database=ThankCard");
            });
            //opt.UseInMemoryDatabase("TodoList"));
            //services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                 
            });

            
        }
        //private void ConfigureRoute(IRouteBuilder routeBuilder)
        //{
        //    //Home/Index 
        //    routeBuilder.MapRoute("CheckUser", "{controller = Users}/{action = CheckUser}/{user_name?}");
        //}
    }
}
