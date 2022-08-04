namespace Huvermann.Extensions.DiNameResolving.ServiceFactories.Playground
{
    public class MiceClass : IAnimalInterface
    {
        public string DoWork(string input)
        {
            return "Piep";
        }

        public string KindOfAnimal()
        {
            return "Mice";
        }
    }
}
