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
    }
}
