# ASP.NET Core Samples

Collection of ASP.NET Core projects caintaining configuration and simple usage of selected .NET services/packages (SignalR, OData, Swagger etc.)

# How to run:

To start needed services (Mongo, Redis, RabbitMQ) just run:

> docker-compose up

# Done:

**1. SignalRChat**:

- basic and advanced usage of SignalR in .NET and JS

**2. RedisCache**:

- Swagger
- CORS
- Caching:
  - distributed - via Redis
  - in-memory
  - reponse/client-side caching - support for GET - "HTTP never supported caching for POST"
- [Windows Secret manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=windows&fbclid=IwAR2nrYRvRMCrU1VyFIECFoyyCaP2OO0e4KRFzMF27S64exgs-xz5xXDerGQ)

**3. MongoCrud**:

- MongoDB CRUD
- [OData](https://www.youtube.com/watch?v=ZCDWUBOJ5FU&list=PL17WHdN9gS1uXtfhSPjGwIxAGGUJqFPWx&index=46&t=0s)
- [AutoMapper](https://automapper.readthedocs.io/en/latest/Getting-started.html) - Entities <-> DTO
- [Docker](http://tattoocoder.com/using-asp-net-core-with-mongodb-in-containers-for-local-dev-cosmosdb-for-production/)
- **Code snippet for "How to integrate Swashbuckle Swagger with OData in ASP.Net Core"**

**4. [MessageBrokers](http://radar.oreilly.com/2015/02/variations-in-event-driven-architecture.html)**:

- <strike>SignalR</strike> - it is rather communication protocol than a way to implement Message Broker
- <strike>MediatR</strike> - it Mediator/**CQRS** library, not Message Broker
- <strike>Redis (publish/subscribe)</strike>
- [MediatR](https://ardalis.com/using-mediatr-in-aspnet-core-apps) (as a CQRS library)
- [RabbitMQ](https://www.rabbitmq.com/getstarted.html)

# Plans:

- ElasticSearch
