FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Shopi.Admin.API/Shopi.Admin.API.csproj", "Shopi.Admin.API/"]
RUN dotnet restore "Shopi.Admin.API/Shopi.Admin.API.csproj"
COPY . .
WORKDIR "/src/Shopi.Admin.API"
RUN dotnet build "Shopi.Admin.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Shopi.Admin.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopi.Admin.API.dll"]
