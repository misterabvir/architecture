#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CloudService.Api/CloudService.Api.csproj", "src/CloudService.Api/"]
COPY ["src/CloudService.Infrastructure/CloudService.Infrastructure.csproj", "src/CloudService.Infrastructure/"]
COPY ["src/CloudService.Application/CloudService.Application.csproj", "src/CloudService.Application/"]
COPY ["src/CloudService.Domain/CloudService.Domain.csproj", "src/CloudService.Domain/"]
RUN dotnet restore "./src/CloudService.Api/CloudService.Api.csproj"
COPY . .
WORKDIR "/src/src/CloudService.Api"
RUN dotnet build "./CloudService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CloudService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudService.Api.dll"]