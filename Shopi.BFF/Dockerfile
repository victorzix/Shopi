FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Shopi.BFF/Shopi.BFF.csproj", "Shopi.BFF/"]
RUN dotnet restore "Shopi.BFF/Shopi.BFF.csproj"
COPY . .
WORKDIR "/src/Shopi.BFF"
RUN dotnet build "Shopi.BFF.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Shopi.BFF.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopi.BFF.dll"]
