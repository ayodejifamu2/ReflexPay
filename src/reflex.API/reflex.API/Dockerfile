FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["reflex.API.csproj", "./"]
# COPY ["ProjectY.Core/ProjectY.Middleware.Core.csproj", "ProjectY.Core/"]
# COPY ["ProjectY.Middleware.Processors/ProjectY.Middleware.Processors.csproj", "ProjectY.Middleware.Processors/"]
# COPY ["ProjectY.Middleware.Shared/ProjectY.Middleware.Shared.csproj", "ProjectY.Middleware.Shared/"]
RUN dotnet restore "reflex.API.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "reflex.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "reflex.API.csproj" -c Release -o /app/publish /p:UseAppHost=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "reflex.API.dll"]