
FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["search.csproj", "."]
RUN dotnet restore "search.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "search.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "search.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apk upgrade

ENTRYPOINT ["dotnet", "search.dll"]
