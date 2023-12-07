using CurrentAccount.Transaction.Application.Transactions.Handlers;
using CurrentAccount.Transaction.Application.Transactions.Services;
using CurrentAccount.Transaction.Core.Transactions;
using CurrentAccount.Transaction.Grpc.Services;
using CurrentAccount.Transaction.Infrastructure.Databases.Contexts;
using CurrentAccount.Transaction.Infrastructure.Databases.Factories;
using CurrentAccount.Transaction.Infrastructure.Databases.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuring dependencies
builder.Services.AddDbContext<TransactionDbContext>(options =>
		options.UseInMemoryDatabase(databaseName: "CurrentAccountTransactionInMemoryDB"));

// Configuring Masstransit with RabbitMq
builder.Services.AddMassTransit(config =>
{
	config.AddConsumer<CreateTransactionEventHandler>();

	config.UsingRabbitMq((context, cfg) =>
	{
		cfg.ConfigureEndpoints(context);
	});
});

builder.Services.AddScoped<ITransactionInfraFactory, TransactionInfraFactory>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ICreateTransactionHandler, CreateTransactionHandler>();
builder.Services.AddScoped<IGetAccountTransactionsHandler, GetAccountTransactionsHandler>();

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AccountTransactionService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
