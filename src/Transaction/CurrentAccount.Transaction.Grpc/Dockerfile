#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Transaction/CurrentAccount.Transaction.Grpc/CurrentAccount.Transaction.Grpc.csproj", "Transaction/CurrentAccount.Transaction.Grpc/"]
COPY ["Transaction/CurrentAccount.Transaction.Application/CurrentAccount.Transaction.Application.csproj", "Transaction/CurrentAccount.Transaction.Application/"]
COPY ["EventBus/EventBus.Messages/EventBus.Messages.csproj", "EventBus/EventBus.Messages/"]
COPY ["Transaction/CurrentAccount.Transaction.Core/CurrentAccount.Transaction.Core.csproj", "Transaction/CurrentAccount.Transaction.Core/"]
COPY ["Shared/CurrentAccount.Core.Shared/CurrentAccount.Core.Shared.csproj", "Shared/CurrentAccount.Core.Shared/"]
COPY ["Transaction/CurrentAccount.Transaction.Infrastructure/CurrentAccount.Transaction.Infrastructure.csproj", "Transaction/CurrentAccount.Transaction.Infrastructure/"]
RUN dotnet restore "Transaction/CurrentAccount.Transaction.Grpc/CurrentAccount.Transaction.Grpc.csproj"
COPY . .
WORKDIR "/src/Transaction/CurrentAccount.Transaction.Grpc"
RUN dotnet build "CurrentAccount.Transaction.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CurrentAccount.Transaction.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrentAccount.Transaction.Grpc.dll"]