using NorthWind.EFCore.Repositories;
using NorthWind.Sales.Controllers;
using NorthWind.Sales.Presenters;
using NorthWind.Sales.UseCases;

namespace NorthWind.Sales.IoC
{

    public static class DependencyContainer
    {
        public static IServiceCollection AddNorthWindSalesServices(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionStringName)
        {
            services.AddRepositories(configuration, connectionStringName)
                .AddUseCasesServices()
                .AddPresenters()
                .AddNorthWindSalesControllers();

            return services;
        }
    }
}
