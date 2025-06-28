# مرحله build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# کپی فایل‌های پروژه
COPY *.sln .
COPY CityExplorer/*.csproj ./CityExplorer/
RUN dotnet restore

COPY . .
WORKDIR /app/CityExplorer
RUN dotnet publish -c Release -o /app/publish

# مرحله runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CityExplorer.dll"]
