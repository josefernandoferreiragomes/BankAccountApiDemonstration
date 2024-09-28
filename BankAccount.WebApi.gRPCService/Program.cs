using BankAccount.WebApi.gRPCService.Services;
using BankAccount.WebAPI.DAL.Repositories;
using BankAccount.WebAPI.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BankAccount.WebApi.BL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<BankServiceImpl>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();


void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    string connectionString = configuration.GetConnectionString("BankAccountDb");
    // Register DbContext, Repositories, and Business Services
    services.AddDbContext<BankAccountContext>(options =>
        options.UseSqlServer(connectionString));

    services.AddScoped<ICustomerRepository, CustomerRepository>();
    services.AddScoped<IAccountRepository, AccountRepository>();
    services.AddScoped<IAccountTransactionRepository, AccountTransactionRepository>();
    
    builder.Services.AddScoped<CustomerService>();  // Register your Business Layer service
    builder.Services.AddScoped<AccountService>();  // Register your Business Layer service
    builder.Services.AddScoped<TransactionService>();  // Register your Business Layer service
}

