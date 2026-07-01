FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ApesDb.slnx", "./"]
COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]
COPY ["global.json", "./"]
COPY ["src/ApesDb.Api/ApesDb.Api.csproj", "src/ApesDb.Api/"]

RUN dotnet restore "src/ApesDb.Api/ApesDb.Api.csproj"

COPY . .

RUN dotnet publish "src/ApesDb.Api/ApesDb.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ApesDb.Api.dll"]
