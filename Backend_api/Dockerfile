#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backend_api/Backend_api.csproj", "Backend_api/"]
COPY ["Backend_core/Backend_core.csproj", "Backend_core/"]
COPY ["Backend_DAL/Backend_DAL.csproj", "Backend_DAL/"]
RUN dotnet restore "./Backend_api/./Backend_api.csproj"
COPY . .
WORKDIR "/src/Backend_api"
RUN dotnet build "./Backend_api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Backend_api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend_api.dll"]