using BankAccount.WebApi.gRPCService.Services;
using BankAccount.WebAPI.DAL.Repositories;
using BankAccount.WebAPI.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();


void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Register DbContext, Repositories, and Business Services
    services.AddDbContext<BankAccountContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("BankAccountDb")));

    services.AddScoped<ICustomerRepository, CustomerRepository>();
    services.AddScoped<IAccountRepository, AccountRepository>();
    services.AddScoped<IAccountTransactionRepository, AccountTransactionRepository>();

}

