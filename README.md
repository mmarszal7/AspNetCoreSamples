# ASP.NET Core Samples
Collection of ASP.NET Core projects caintaining configuration and simple usage of selected .NET services/packages (SignalR, OData, Swagger etc.)

# Done:
SignalRChat:
- basic and advanced usage of SignalR in .NET and JS

RedisCache:
- Swagger 
- CORS
- Caching:
	- distributed - via Redis
	- in-memory
	- reponse/client-side caching - support for GET - "HTTP never supported caching for POST"
- [Windows Secret manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=windows&fbclid=IwAR2nrYRvRMCrU1VyFIECFoyyCaP2OO0e4KRFzMF27S64exgs-xz5xXDerGQ)

MongoCrud:
- MongoDB.Driver CRUD
- [OData](https://www.youtube.com/watch?v=ZCDWUBOJ5FU&list=PL17WHdN9gS1uXtfhSPjGwIxAGGUJqFPWx&index=46&t=0s)
- [AutoMapper](https://automapper.readthedocs.io/en/latest/Getting-started.html) - Entities <-> DTO
- [Docker](http://tattoocoder.com/using-asp-net-core-with-mongodb-in-containers-for-local-dev-cosmosdb-for-production/)
+ Code snippet for "How to integrate Swashbuckle Swagger with OData in ASP.Net Core"

# Plans:
- ElasticSearch
- publish/subscribe with Redis & RabbitMQ (RawRabbit)
- make notes about used services: Mongo, RabbitMQ, ElasticSearch