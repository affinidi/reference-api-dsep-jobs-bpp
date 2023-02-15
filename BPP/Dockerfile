FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV DOTNET_CLI_TELEMETRY_OPTOUT 1

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out
copy ./StaticFiles/Xinput.txt /app/out/StaticFiles/Xinput.txt
# build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build-env /app/out .

RUN apk upgrade

ENTRYPOINT ["dotnet", "bpp.dll"]
