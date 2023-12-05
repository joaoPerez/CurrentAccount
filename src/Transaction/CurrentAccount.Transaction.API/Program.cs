using CurrentAccount.Transaction.Application.Transactions.Handlers;
using CurrentAccount.Transaction.Application.Transactions.Services;
using CurrentAccount.Transaction.Core.Transactions;
using CurrentAccount.Transaction.Infrastructure.Databases.Contexts;
using CurrentAccount.Transaction.Infrastructure.Databases.Factories;
using CurrentAccount.Transaction.Infrastructure.Databases.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITransactionInfraFactory, TransactionInfraFactory>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ICreateTransactionHandler, CreateTransactionHandler>();

builder.Services.AddDbContext<TransactionContext>(options =>
		options.UseInMemoryDatabase(databaseName: "CurrentAccountTransactionInMemoryDB"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
