FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
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