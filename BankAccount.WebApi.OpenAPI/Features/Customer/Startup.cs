using BankAccount.WebApi.BL;
using BankAccount.WebAPI.DAL.Repositories;
using BankAccount.WebAPI.DAL;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BankAccount.WebApi.OpenAPI.Features
{
    internal static class Startup
    {
        public static void AddCustomerServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("BankAccountDb");
            // Register DbContext, Repositories, and Business Services
            services.AddDbContext<BankAccountContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            //services.AddScoped<IAccountRepository, AccountRepository>();
            //services.AddScoped<IAccountTransactionRepository, AccountTransactionRepository>();

            services.AddScoped<ICustomerService, CustomerService>();  // Register your Business Layer service
            //builder.Services.AddScoped<AccountService>();  // Register your Business Layer service
            //builder.Services.AddScoped<TransactionService>();  // Register your Business Layer service

        }
    }
}
