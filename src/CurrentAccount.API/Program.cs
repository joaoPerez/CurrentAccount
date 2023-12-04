using CurrentAccount.Application.CurrentAccount.Handlers;
using CurrentAccount.Application.CurrentAccount.Services;
using CurrentAccount.Application.Customer;
using CurrentAccount.Core.CurrentAccount;
using CurrentAccount.Core.Customer;
using CurrentAccount.Infrastructure.Database.Context;
using CurrentAccount.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICurrentAccountInfraFactory, CurrentAccountInfraFactory>();
builder.Services.AddScoped<ICreateCurrentAccountHandler, CreateCurrentAccountHandler>();
builder.Services.AddScoped<ICurrentAccountRepository, CurrentAccountRepository>();
builder.Services.AddScoped<ICurrentAccountService, CurrentAccountService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

// Because this is an example I'm allowing all origins.
builder.Services.AddCors(options =>
{
	options.AddPolicy("example_policy",
		builder => builder.WithOrigins("")
							.AllowAnyHeader()
							.AllowAnyMethod());
	options.AddPolicy("AllowAll",
		builder => builder.AllowAnyOrigin()
						  .AllowAnyHeader()
						  .AllowAnyMethod());
});

builder.Services.AddDbContext<CurrentAccountContext>(options =>
		options.UseInMemoryDatabase(databaseName: "CurrentAccountInMemoryDB"));

var app = builder.Build();

using var scope = app.Services.CreateScope();
CurrentAccountContext dbcontext = scope.ServiceProvider.GetRequiredService<CurrentAccountContext>();
dbcontext.Database.EnsureCreated();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
