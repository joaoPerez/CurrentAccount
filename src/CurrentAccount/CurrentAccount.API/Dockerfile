#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CurrentAccount/CurrentAccount.API/CurrentAccount.API.csproj", "CurrentAccount/CurrentAccount.API/"]
COPY ["CurrentAccount/CurrentAccount.Application/CurrentAccount.Application.csproj", "CurrentAccount/CurrentAccount.Application/"]
COPY ["CurrentAccount/CurrentAccount.Core/CurrentAccount.Core.csproj", "CurrentAccount/CurrentAccount.Core/"]
COPY ["Shared/CurrentAccount.Core.Shared/CurrentAccount.Core.Shared.csproj", "Shared/CurrentAccount.Core.Shared/"]
COPY ["CurrentAccount/CurrentAccount.Infrastructure/CurrentAccount.Infrastructure.csproj", "CurrentAccount/CurrentAccount.Infrastructure/"]
RUN dotnet restore "CurrentAccount/CurrentAccount.API/CurrentAccount.API.csproj"
COPY . .
WORKDIR "/src/CurrentAccount/CurrentAccount.API"
RUN dotnet build "CurrentAccount.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CurrentAccount.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrentAccount.API.dll"]