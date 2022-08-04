using Huvermann.Extension.DiNameResolving;
using System;

namespace Huvermann.Extensions.DiNameResolving.ServiceFactories.Playground
{
    public class AnimalShelter
    {
        private readonly IServiceByNameFactory<IAnimalInterface> _animalfactory;

        public AnimalShelter(IServiceByNameFactory<IAnimalInterface> animalfactory)
        {
            _animalfactory = animalfactory;
        }

        public IServiceByNameFactory<IAnimalInterface> GetFactory()
        {
            return _animalfactory;
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
