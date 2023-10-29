#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Microservices/Gateway/2.WebApi/Demostrativo.Ocelot.WebApi/Demostrativo.Ocelot.WebApi.csproj", "src/Microservices/Gateway/2.WebApi/Demostrativo.Ocelot.WebApi/"]
RUN dotnet restore "src/Microservices/Gateway/2.WebApi/Demostrativo.Ocelot.WebApi/Demostrativo.Ocelot.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Microservices/Gateway/2.WebApi/Demostrativo.Ocelot.WebApi"
RUN dotnet build "Demostrativo.Ocelot.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demostrativo.Ocelot.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demostrativo.Ocelot.WebApi.dll"]