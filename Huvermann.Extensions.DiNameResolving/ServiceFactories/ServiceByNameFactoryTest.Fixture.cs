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

            
        }
    }
}
