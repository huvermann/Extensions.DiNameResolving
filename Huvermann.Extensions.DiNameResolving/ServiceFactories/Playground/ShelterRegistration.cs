using Huvermann.Extension.DiNameResolving;
using Microsoft.Extensions.DependencyInjection;

namespace Huvermann.Extensions.DiNameResolving.ServiceFactories.Playground
{
    public static class ShelterRegistration
    {
        public static IServiceCollection RegisterShelter(this IServiceCollection services)
        {
            // Register the extension
            services.AddNamedServiceRegistration();
            
            // Register the factory.
            services.AddTransient<IServiceByNameFactory<IAnimalInterface>, ServiceByNameFactory<IAnimalInterface>>();
            
            // Register by name.
            services.AddTransientByName<IAnimalInterface, CatClass>("Cat");
            services.AddTransientByName<IAnimalInterface, DogClass>("Dog");
            
            // Register the class that uses the factory.
            services.AddTransient<AnimalShelter>();

            return services;
        }
    }
}
