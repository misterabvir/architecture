# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Authentications/RobotCloudService.Authentications.Presentation/RobotCloudService.Authentications.Presentation.csproj", "src/Authentications/RobotCloudService.Authentications.Presentation/"]
COPY ["src/RobotCloudService.Contracts/RobotCloudService.Contracts.csproj", "src/RobotCloudService.Contracts/"]
COPY ["src/Authentications/RobotCloudService.Authentications.Infrastructure/RobotCloudService.Authentications.Infrastructure.csproj", "src/Authentications/RobotCloudService.Authentications.Infrastructure/"]
COPY ["src/Authentications/RobotCloudService.Authentications.Application/RobotCloudService.Authentications.Application.csproj", "src/Authentications/RobotCloudService.Authentications.Application/"]
COPY ["src/RobotCloudService.Application/RobotCloudService.Application.csproj", "src/RobotCloudService.Application/"]
COPY ["src/RobotCloudService.Notifications/RobotCloudService.Notifications.csproj", "src/RobotCloudService.Notifications/"]
RUN dotnet restore "./src/Authentications/RobotCloudService.Authentications.Presentation/RobotCloudService.Authentications.Presentation.csproj"
COPY . .
WORKDIR "/src/src/Authentications/RobotCloudService.Authentications.Presentation"
RUN dotnet build "./RobotCloudService.Authentications.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RobotCloudService.Authentications.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RobotCloudService.Authentications.Presentation.dll"]