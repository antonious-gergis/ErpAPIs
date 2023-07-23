using APIV2.Mark.Business.Services;
using APIV2.Mark.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace APIV2.Mark.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IChartOfAccountService, ChartOfAccountService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<ITaxService, TaxService>();
            services.AddTransient<IJournalService, JournalService>();
            services.AddScoped<ITreasuryService, TreasuryService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ITransactionOperationsService, TransactionOperationsService>();
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
