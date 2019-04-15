using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoCRUD.Model;
using MongoDB.Driver;
using System;
using System.Linq;

namespace MongoCRUD
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

            // MongoDB - default connectionString: mongodb://localhost:27017
            var mongoClient = new MongoClient(Configuration["MongoConnection"]);
            var db = mongoClient.GetDatabase("demoDatabase");
            services.AddScoped<IMongoDatabase>(_ => db);
            PopulateDatabase(db);

            // OData
            services.AddOData();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routeBuilder =>
            {
                // OData
                routeBuilder.EnableDependencyInjection();
                routeBuilder.Expand().Select().Count().OrderBy().Filter();
            });
        }

        private void PopulateDatabase(IMongoDatabase MongoDatabase)
        {
            MongoDatabase.DropCollection("Records");
            MongoDatabase.GetCollection<Record>("Records")
                .InsertManyAsync((new int[] { 0, 1, 2, 3, 4 })
                    .Select(i => new Record() { Id = i, Value = "test" + i, Timestamp = DateTime.Now.AddDays(i) })
                );

            MongoDatabase.DropCollection("Events");
            MongoDatabase.GetCollection<Event>("Events")
                .InsertManyAsync((new int[] { 0, 1, 2, 3, 4 })
                    .Select(i => new Event() { Id = i, Reason = "Reason number " + i })
                );
        }
    }
}
