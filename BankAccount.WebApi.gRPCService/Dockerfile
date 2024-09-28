#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BankAccount.WebApi.gRPCService/BankAccount.WebApi.gRPCService.csproj", "BankAccount.WebApi.gRPCService/"]
RUN dotnet restore "./BankAccount.WebApi.gRPCService/BankAccount.WebApi.gRPCService.csproj"
COPY . .
WORKDIR "/src/BankAccount.WebApi.gRPCService"
RUN dotnet build "./BankAccount.WebApi.gRPCService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BankAccount.WebApi.gRPCService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM ubuntu:latest
USER root
RUN mkdir -p /var/lib/apt/lists/partial && chmod -R 755 /var/lib/apt/lists
RUN apt-get update && apt-get install -y curl apt-transport-https

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankAccount.WebApi.gRPCService.dll"]

