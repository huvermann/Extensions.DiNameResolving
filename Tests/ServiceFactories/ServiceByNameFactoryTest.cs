using Huvermann.Extensions.DiNameResolving.ServiceFactories.Playground;
using System;
using Xunit;

namespace Huvermann.Extensions.DiNameResolving.ServiceFactories
{
    public partial class ServiceByNameFactoryTest
    {
        /// <summary>
        /// Check if AnimalShelter can be constructed with injected dependency
        /// </summary>
        [Fact]
        public void JustConstructorTest()
        {
            Fixture fixture = new Fixture()
                .WithShelterRegistration();

            AnimalShelter? testObject = fixture.Build();
            Assert.IsType<AnimalShelter>(testObject);
        }

        /// <summary>
        /// Check if dog class can be resolved by name "Dog" and check if the kindofanimal returns "Dog"
        /// </summary>
        [Fact]
        public void ResolveDogByName()
        {
            var factory = new Fixture()
                .WithShelterRegistration()
                .Build()?
                .GetFactory();

            var actual = factory?.GetServiceByName("Dog");
            Assert.Equal("Dog", actual?.KindOfAnimal());

        }

        /// <summary>
        /// Check if cat class can be resolved by name "Cat" and check if the kindofanimal returns "Cat"
        /// </summary>
        [Fact]
        public void ResolveCatByName()
        {
            var factory = new Fixture()
                .WithShelterRegistration()
                .Build()?
                .GetFactory();

            var actual = factory?.GetServiceByName("Cat");
            Assert.Equal("Cat", actual?.KindOfAnimal());

        }

        /// <summary>
        /// Check wether unregistered name "Mice" returns exception.
        /// </summary>
        [Fact]
        public void ResolveUnknownNameThrows()
        {
            var factory = new Fixture()
                .WithShelterRegistration()
                .Build()?
                .GetFactory();
            Assert.Throws<InvalidOperationException>(() =>  {
                var actual = factory?.GetServiceByName("Mice");
            });
        }

        /// <summary>
        /// Register a class Mice and check if it behaves like a mice.
        /// </summary>
        [Fact]
        public void RegisterAndResolvMice()
        {
            var factory = new Fixture()
                .WithRegisterMice()
                .Build()?
                .GetFactory();

            var actual = factory?.GetServiceByName("Mice");
            Assert.Equal("Mice", actual?.KindOfAnimal());
            Assert.Equal("Piep", actual?.DoWork(""));

        }

        /// <summary>
        /// Check two instances are not the same.
        /// </summary>
        [Fact]
        public void ResolveTwoDogs_NotSame()
        {
            var factory = new Fixture()
                .WithShelterRegistration()
                .Build()?
                .GetFactory();

            var dog1 = factory.GetServiceByName("Dog");
            var dog2 = factory.GetServiceByName("Dog");
            Assert.NotSame(dog1, dog2);
        }

        /// <summary>
        /// Check two instances are same, if they are registered as singleton
        /// </summary>
        [Fact]
        public void ResolveTwoDogs_Same()
        {
            var factory = new Fixture()
                .WithSingletonRegistration()
                .Build()?
                .GetFactory();

            var dog1 = factory.GetServiceByName("Dog");
            var dog2 = factory.GetServiceByName("Dog");
            Assert.Same(dog1, dog2);
        }

        [Fact]
        public void ResolveTwoDogsTwoMice_SameMiceDifferentDogs()
        {
            var factory = new Fixture()
                .WithSingletonAndTransistentRegistration()
                .Build()?
                .GetFactory();

            var dog1 = factory.GetServiceByName("Dog");
            var dog2 = factory.GetServiceByName("Dog");
            var mice1 = factory.GetServiceByName("Mice");
            var mice2 = factory.GetServiceByName("Mice");
            // Dog is registered as singleton
            Assert.Same(dog1, dog2);
            // Mice has been registered as transistent
            Assert.NotSame(mice1, mice2);

        }

        [Fact]
        public void Scoped()
        {
            var fixture = new Fixture()
                .WithScopedRegistration();
            var scope1 = fixture.BuildSopedAnimalShelter();
            var scope2 = fixture.BuildSopedAnimalShelter();

            var dog1fromscope1 = scope1.GetFactory().GetServiceByName("Dog");
            var dog2fromscope1 = scope1.GetFactory().GetServiceByName("Dog");
            var dog1fromscope2 = scope2.GetFactory().GetServiceByName("Dog");
            var dog2fromscope2 = scope2.GetFactory().GetServiceByName("Dog");

            // from same scope
            Assert.Same(dog1fromscope1 , dog2fromscope1);
            Assert.Same(dog1fromscope2 , dog2fromscope2);

            // not from same scope
            Assert.NotSame(dog1fromscope1, dog1fromscope2);
            Assert.NotSame(dog2fromscope1, dog2fromscope2);

        }
    }
}
