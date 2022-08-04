namespace Huvermann.Extensions.DiNameResolving.ServiceFactories.Playground
{
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
}
