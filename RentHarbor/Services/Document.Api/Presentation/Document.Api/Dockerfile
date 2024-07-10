#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Document.Api/Presentation/Document.Api/Document.Api.csproj", "Services/Document.Api/Presentation/Document.Api/"]
COPY ["Services/Shared/RentHarbor.AuthService/RentHarbor.AuthService.csproj", "Services/Shared/RentHarbor.AuthService/"]
COPY ["Services/Document.Api/Application/Document.Application/Document.Application.csproj", "Services/Document.Api/Application/Document.Application/"]
COPY ["Services/Document.Api/Persistance/Document.Persistance/Document.Persistance.csproj", "Services/Document.Api/Persistance/Document.Persistance/"]
COPY ["Services/Shared/RentHarbor.MongoDb/RentHarbor.MongoDb.csproj", "Services/Shared/RentHarbor.MongoDb/"]
RUN dotnet restore "./Services/Document.Api/Presentation/Document.Api/Document.Api.csproj"
COPY . .
WORKDIR "/src/Services/Document.Api/Presentation/Document.Api"
RUN dotnet build "./Document.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Document.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Document.Api.dll"]