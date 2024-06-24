#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Ordering/Ordering.Api/Ordering.Api/Ordering.Api.csproj", "Services/Ordering/Ordering.Api/Ordering.Api/"]
COPY ["Services/Shared/RentHarbor.AuthService/RentHarbor.AuthService.csproj", "Services/Shared/RentHarbor.AuthService/"]
COPY ["Services/Ordering/Ordering.Application/Ordering.Application.csproj", "Services/Ordering/Ordering.Application/"]
COPY ["Services/Ordering/Ordering.Persistance/Ordering.Persistance.csproj", "Services/Ordering/Ordering.Persistance/"]
COPY ["Services/Shared/RentHarbor.MongoDb/RentHarbor.MongoDb.csproj", "Services/Shared/RentHarbor.MongoDb/"]
RUN dotnet restore "./Services/Ordering/Ordering.Api/Ordering.Api/Ordering.Api.csproj"
COPY . .
WORKDIR "/src/Services/Ordering/Ordering.Api/Ordering.Api"
RUN dotnet build "./Ordering.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ordering.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.Api.dll"]