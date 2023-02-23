
FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["DAL-API.csproj", "."]
RUN dotnet restore "DAL-API.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "DAL-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DAL-API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apk upgrade

ENTRYPOINT ["dotnet", "DAL-API.dll"]
