#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Communication.Api/Presentation/Communication.Api.csproj", "Communication.Api/Presentation/"]
COPY ["Shared/RentHarbor.AuthService/RentHarbor.AuthService.csproj", "Shared/RentHarbor.AuthService/"]
COPY ["Communication.Api/Application/Communication.Application/Communication.Application.csproj", "Communication.Api/Application/Communication.Application/"]
COPY ["Communication.Api/Persistance/Communication.Persistance/Communication.Persistance.csproj", "Communication.Api/Persistance/Communication.Persistance/"]
COPY ["Shared/RentHarbor.MongoDb/RentHarbor.MongoDb.csproj", "Shared/RentHarbor.MongoDb/"]
RUN dotnet restore "./Communication.Api/Presentation/Communication.Api.csproj"
COPY . .
WORKDIR "/src/Communication.Api/Presentation"
RUN dotnet build "./Communication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Communication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Communication.Api.dll"]