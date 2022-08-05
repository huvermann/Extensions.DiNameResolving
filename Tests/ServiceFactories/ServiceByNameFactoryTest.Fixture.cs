using Huvermann.Extension.DiNameResolving;
using Huvermann.Extensions.DiNameResolving.ServiceFactories.Playground;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huvermann.Extensions.DiNameResolving.ServiceFactories
{
    public partial class ServiceByNameFactoryTest
    {
        class Fixture
        {

            private readonly IServiceCollection _serviceCollection = new ServiceCollection();

            internal Fixture WithShelterRegistration()
            {
                _serviceCollection.RemoveAllNameRegs();
                _serviceCollection.RegisterShelter();
                return this;
            }

            internal Fixture WithSingletonRegistration()
            {
                _serviceCollection.RemoveAllNameRegs();
                var services = _serviceCollection;
                services.AddNamedServiceRegistration();
                // Register the factory.
                services.AddTransient<IServiceByNameFactory<IAnimalInterface>, ServiceByNameFactory<IAnimalInterface>>();

                services.AddSingletonByName<IAnimalInterface, DogClass>("Dog");
                services.AddSingletonByName<IAnimalInterface, CatClass>("Cat");

                // Register the class that uses the factory.
                services.AddTransient<AnimalShelter>();
                return this;
            }

            internal Fixture WithSingletonAndTransistentRegistration()
            {
                _serviceCollection.RemoveAllNameRegs();
                var services = _serviceCollection;
                services.AddNamedServiceRegistration();
                // Register the factory.
                services.AddTransient<IServiceByNameFactory<IAnimalInterface>, ServiceByNameFactory<IAnimalInterface>>();

                services.AddSingletonByName<IAnimalInterface, DogClass>("Dog");
                services.AddSingletonByName<IAnimalInterface, CatClass>("Cat");
                services.AddTransientByName<IAnimalInterface, MiceClass>("Mice");

                // Register the class that uses the factory.
                services.AddTransient<AnimalShelter>();
                return this;
            }

            internal Fixture WithScopedRegistration()
            {
                _serviceCollection.RemoveAllNameRegs();
                var services = _serviceCollection;
                services.AddNamedServiceRegistration();
                services.AddTransient<IServiceByNameFactory<IAnimalInterface>, ServiceByNameFactory<IAnimalInterface>>();

                services.AddScopedByName<IAnimalInterface, DogClass>("Dog");
                // Register the class that uses the factory.
                services.AddScoped<AnimalShelter>();
                return this;
            }

            internal Fixture WithRegisterMice()
            {
                var services = _serviceCollection;
                // Register the extension
                services.AddNamedServiceRegistration();

                // Register the factory.
                services.AddTransient<IServiceByNameFactory<IAnimalInterface>, ServiceByNameFactory<IAnimalInterface>>();

                // Register by name.
                services.AddTransientByName<IAnimalInterface, MiceClass>("Mice");

                // Register the class that uses the factory.
                services.AddTransient<AnimalShelter>();
                return this;
            }
            internal AnimalShelter? Build()
            {

                IServiceProvider provider = _serviceCollection.BuildServiceProvider();
                return provider.GetService<AnimalShelter>();
            }

            internal AnimalShelter? BuildSopedAnimalShelter()
            {
                IServiceProvider provider = _serviceCollection.BuildServiceProvider();
                var scopeFactory = provider.GetService<IServiceScopeFactory>();
                var scope = scopeFactory?.CreateScope();
                return scope?.ServiceProvider.GetService<AnimalShelter>();
            }


        }
    }
}
