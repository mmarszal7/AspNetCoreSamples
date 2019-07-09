using AutoMapper;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using MongoCRUD.Model;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
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
            services.AddMvc()
                // PascalCase reponse 
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Swagger
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "API docs", Version = "v1" }));

            // MongoDB - default connectionString: mongodb://localhost:27017
            // MongoDB - docker connectionString: mongodb://mongo:27017
            var mongoClient = new MongoClient(Configuration["MongoConnection"]);
            var db = mongoClient.GetDatabase("demoDatabase");
            services.AddScoped<IMongoDatabase>(_ => db);
            PopulateDatabase(db);

            // OData
            services.AddOData();

            // OData + Swagger workaround (good enough for testing purposes ONLY!)
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API docs");
            });

            // Automapper
            Mapper.Initialize(cfg =>
                cfg.CreateMap<Population, ResponseDTO>()
                    .ForMember(d => d.Area, opt => opt.MapFrom(src => src.District))
                    .ForMember(d => d.Year1970, opt => opt.MapFrom(src => src.Year["1970"]))
                    .ForMember(d => d.Year1980, opt => opt.MapFrom(src => src.Year["1980"]))
                    .ForMember(d => d.Year1990, opt => opt.MapFrom(src => src.Year["1990"]))
                    .ForMember(d => d.Year2000, opt => opt.MapFrom(src => src.Year["2000"]))
                    .ForMember(d => d.Year2010, opt => opt.MapFrom(src => src.Year["2010"]))
                    .ForMember(d => d.Max, opt => opt.MapFrom(src => new Dictionary<string, int>() { { src.Year.First(y => y.Value.Equals(src.Year.Values.Max())).Key, src.Year.Values.Max() } }))
                    .ForMember(d => d.Min, opt => opt.MapFrom(src => new Dictionary<string, int>() { { src.Year.First(y => y.Value.Equals(src.Year.Values.Min())).Key, src.Year.Values.Min() } }))
                    .ForMember(d => d.Average, opt => opt.MapFrom(src => src.Year.Values.Average()))
            );

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

            var json = File.ReadAllText("./populationByDistrict.json");
            var population = JsonConvert.DeserializeObject<List<Population>>(json);
            MongoDatabase.DropCollection("Population");
            MongoDatabase.GetCollection<Population>("Population")
                .InsertManyAsync(population)
                .Wait();
        }
    }
}
