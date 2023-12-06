#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 9090
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Transaction/CurrentAccount.Transaction.API/CurrentAccount.Transaction.API.csproj", "Transaction/CurrentAccount.Transaction.API/"]
COPY ["Transaction/CurrentAccount.Transaction.Application/CurrentAccount.Transaction.Application.csproj", "Transaction/CurrentAccount.Transaction.Application/"]
COPY ["Transaction/CurrentAccount.Transaction.Core/CurrentAccount.Transaction.Core.csproj", "Transaction/CurrentAccount.Transaction.Core/"]
COPY ["Shared/CurrentAccount.Core.Shared/CurrentAccount.Core.Shared.csproj", "Shared/CurrentAccount.Core.Shared/"]
COPY ["Transaction/CurrentAccount.Transaction.Infrastructure/CurrentAccount.Transaction.Infrastructure.csproj", "Transaction/CurrentAccount.Transaction.Infrastructure/"]
RUN dotnet restore "Transaction/CurrentAccount.Transaction.API/CurrentAccount.Transaction.API.csproj"
COPY . .
WORKDIR "/src/Transaction/CurrentAccount.Transaction.API"
RUN dotnet build "CurrentAccount.Transaction.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CurrentAccount.Transaction.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrentAccount.Transaction.API.dll"]