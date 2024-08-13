#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ATS.API/ATS.API.csproj", "ATS.API/"]
COPY ["ATS.Data/ATS.Data.csproj", "ATS.Data/"]
COPY ["ATS.Model/ATS.Model.csproj", "ATS.Model/"]
COPY ["ATS.IServices/ATS.IServices.csproj", "ATS.IServices/"]
COPY ["ATS.DTO/ATS.DTO.csproj", "ATS.DTO/"]
COPY ["ATS.IRepository/ATS.IRepository.csproj", "ATS.IRepository/"]
COPY ["ATS.Repository/ATS.Repository.csproj", "ATS.Repository/"]
COPY ["ATS.Services/ATS.Services.csproj", "ATS.Services/"]
COPY ["ATS.Hubs/ATS.Hubs.csproj", "ATS.Hubs/"]
RUN dotnet restore "./ATS.API/ATS.API.csproj"
COPY . .
WORKDIR "/src/ATS.API"
RUN dotnet build "./ATS.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ATS.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ATS.API.dll"]