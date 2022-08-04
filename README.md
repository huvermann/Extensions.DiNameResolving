# Extensions.DiNameResolving
Lightweight DI extension to add names to class registrations.

DI-Container registration example:
```charp
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
```



All animal classes implementing the same interface:

```csharp

public interface IAnimalInterface
    {
        public string DoWork(string input);
        public string KindOfAnimal();
    }

    public class DogClass : IAnimalInterface
    {
        public string DoWork(string input)
        {
            return "Wuff";
        }

        public string KindOfAnimal()
        {
            return "Dog";
        }
    }

    public class CatClass : IAnimalInterface
    {
        public string DoWork(string input)
        {
            return "Miauuu";
        }

        public string KindOfAnimal()
        {
            return "Cat";
        }
    }
```

Now we want to inject the ServiceByNameFactory to the AnimalShelter-Class
And use them:

```csharp
using Huvermann.Extension.DiNameResolving;

namespace Huvermann.Extensions.DiNameResolving.ServiceFactories.Playground
{
    public class AnimalShelter
    {
        private IServiceByNameFactory<IAnimalInterface> _animalfactory;

        public AnimalShelter(IServiceByNameFactory<IAnimalInterface> animalfactory)
        {
            _animalfactory = animalfactory;
        }

        public void CreateSomeAnimals()
        {
            var dog = _animalfactory.GetServiceByName("Dog");
            Console.WriteLine(dog.KindOfAnimal());
            dog.DoWork("Something");

            var cat = _animalfactory.GetServiceByName("Cat");
            Console.WriteLine(cat.KindOfAnimal());
            cat.DoWork("Something");
        }
    }
}
```



