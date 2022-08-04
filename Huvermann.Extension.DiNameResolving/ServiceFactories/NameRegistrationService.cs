using System;
using System.Collections.Generic;
using System.Text;

namespace Huvermann.Extension.NameResolver.ServiceFactories
{
    internal class NameRegistrationService : INameRegistrationService
    {
        private readonly static Dictionary<string, Dictionary<string, Func<IServiceProvider, object>>> _registry = new Dictionary<string, Dictionary<string, Func<IServiceProvider, object>>>();
        public Dictionary<string, Dictionary<string, Func<IServiceProvider, object>>> NameRegistry => NameRegistrationService._registry;
    }
}
