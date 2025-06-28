# مرحله build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# کپی فایل راه‌حل و همه پروژه‌ها
COPY *.sln .
COPY Domain/*.csproj ./Domain/
COPY Application/*.csproj ./Application/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY CityExplorer/*.csproj ./CityExplorer/

# Restore کردن همه پروژه‌ها
RUN dotnet restore

# کپی کامل همه فایل‌ها
COPY . .

# تنظیم دایرکتوری کاری به فولدر پروژه اصلی
WORKDIR /app/CityExplorer

# بیلد و پابلیش پروژه اصلی
RUN dotnet publish -c Release -o /app/publish

# مرحله runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# کپی فایل‌های منتشر شده از مرحله build
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CityExplorer.dll"]
