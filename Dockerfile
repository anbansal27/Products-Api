#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Products.Api/Products.Api.csproj", "src/Products.Api/"]
COPY ["src/Products.Api.Application/Products.Api.Application.csproj", "src/Products.Api.Application/"]
COPY ["src/Products.Api.Data/Products.Api.Data.csproj", "src/Products.Api.Data/"]
COPY ["src/Products.Api.Entities/Products.Api.Entities.csproj", "src/Products.Api.Entities/"]
RUN dotnet restore "src/Products.Api/Products.Api.csproj"
COPY . .
WORKDIR "/src/src/Products.Api"
RUN dotnet build "Products.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Products.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Products.Api.dll"]