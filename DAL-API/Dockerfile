FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
WORKDIR /app
EXPOSE 8080
EXPOSE 443

ENV DOTNET_CLI_TELEMETRY_OPTOUT 1

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out
#copy ./StaticFiles/Xinput.txt /app/out/StaticFiles/Xinput.txt
# build runtime image

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
RUN apk upgrade
RUN adduser \
  --disabled-password \
  --home /app \
  --gecos '' app \
  && chown -R app /app
USER app
WORKDIR /app
COPY --from=build-env /app/out .


ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 \
  DOTNET_RUNNING_IN_CONTAINER=true \
  ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "DAL-API.dll"]


#FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:3.1-alpine AS build
#WORKDIR /src
#COPY ["nuget.config", "."]
#COPY ["DAL-API.csproj", "."]
#RUN dotnet restore "DAL-API.csproj"
#COPY . .
#WORKDIR "/src"
#RUN dotnet build "DAL-API.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "DAL-API.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#RUN apk upgrade
#
#ENTRYPOINT ["dotnet", "DAL-API.dll"]

