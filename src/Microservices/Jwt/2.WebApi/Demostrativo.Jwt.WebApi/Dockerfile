#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Microservices/Jwt/2.WebApi/Demostrativo.Jwt.WebApi/Demostrativo.Jwt.WebApi.csproj", "src/Microservices/Jwt/2.WebApi/Demostrativo.Jwt.WebApi/"]
COPY ["src/Brokers/RabbitMQ/Infra.Bus/Micro.Infra.Bus/Micro.Infra.Bus.csproj", "src/Brokers/RabbitMQ/Infra.Bus/Micro.Infra.Bus/"]
COPY ["src/Brokers/RabbitMQ/Domain/Micro.Domain.Core/Micro.Domain.Core.csproj", "src/Brokers/RabbitMQ/Domain/Micro.Domain.Core/"]
COPY ["src/Brokers/RabbitMQ/Infra.IoC/Micro.Infra.IoC/Micro.Infra.IoC.csproj", "src/Brokers/RabbitMQ/Infra.IoC/Micro.Infra.IoC/"]
COPY ["src/Microservices/Jwt/3.Aplication/Demostrativo.Jwt.Aplication.Core/Demostrativo.Jwt.Aplication.Core.csproj", "src/Microservices/Jwt/3.Aplication/Demostrativo.Jwt.Aplication.Core/"]
COPY ["src/Microservices/Jwt/5.Infrastructure/5.1.Data/Demostrativo.Jwt.Persistence/Demostrativo.Jwt.Persistence.csproj", "src/Microservices/Jwt/5.Infrastructure/5.1.Data/Demostrativo.Jwt.Persistence/"]
COPY ["src/Microservices/Jwt/4.Domain/Demostrativo.Jwt.Domain/Demostrativo.Jwt.Domain.csproj", "src/Microservices/Jwt/4.Domain/Demostrativo.Jwt.Domain/"]
COPY ["src/Microservices/Jwt/3.Aplication/Demostrativo.Jwt.Aplication.Dto/Demostrativo.Jwt.Aplication.Dto.csproj", "src/Microservices/Jwt/3.Aplication/Demostrativo.Jwt.Aplication.Dto/"]
RUN dotnet restore "src/Microservices/Jwt/2.WebApi/Demostrativo.Jwt.WebApi/Demostrativo.Jwt.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Microservices/Jwt/2.WebApi/Demostrativo.Jwt.WebApi"
RUN dotnet build "Demostrativo.Jwt.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demostrativo.Jwt.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demostrativo.Jwt.WebApi.dll"]