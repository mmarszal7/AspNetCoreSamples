using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Swagger;

namespace RedisCache
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // CORS
            services.AddCors(o => o.AddPolicy("EverythingOpened", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            // Redis Cache
            var connectionMultiplexer = ConnectionMultiplexer.Connect(Configuration["RedisConnection"]);
            services.AddScoped<IDatabase>(_ => connectionMultiplexer.GetDatabase(0));

            // Memory Cache
            services.AddMemoryCache();

            // Response/Client-side cache
            services.AddResponseCaching();

            // Swagger
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "API docs", Version = "v1" }));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("AllowAll");
            }

            // Response/Client-side cache
            app.UseResponseCaching();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API docs");
            });

            app.UseMvc();
        }
    }
}
