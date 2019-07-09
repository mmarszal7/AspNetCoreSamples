FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["MongoCRUD/MongoCRUD.csproj", "MongoCRUD/"]
RUN dotnet restore "MongoCRUD/MongoCRUD.csproj"
COPY . .
WORKDIR "/src/MongoCRUD"
RUN dotnet build "MongoCRUD.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MongoCRUD.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MongoCRUD.dll"]