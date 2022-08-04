using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huvermann.Extensions.DiNameResolving.ServiceFactories.Playground
{
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
}
