FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Lab5/Infrastructure/Lab5.Infrastructure.DataAccess/Lab5.Infrastructure.DataAccess.csproj", "Lab5/Infrastructure/Lab5.Infrastructure.DataAccess/"]
RUN dotnet restore "Lab5/Infrastructure/Lab5.Infrastructure.DataAccess/Lab5.Infrastructure.DataAccess.csproj"
COPY . .
WORKDIR "/src/Lab5/Infrastructure/Lab5.Infrastructure.DataAccess"
RUN dotnet build "Lab5.Infrastructure.DataAccess.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lab5.Infrastructure.DataAccess.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab5.Infrastructure.DataAccess.dll"]
