FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Shopi.Customer.API/Shopi.Customer.API.csproj", "Shopi.Customer.API/"]
RUN dotnet restore "Shopi.Customer.API/Shopi.Customer.API.csproj"
COPY . .
WORKDIR "/src/Shopi.Customer.API"
RUN dotnet build "Shopi.Customer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Shopi.Customer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopi.Customer.API.dll"]
