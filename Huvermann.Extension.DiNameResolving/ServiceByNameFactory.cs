using Huvermann.Extension.NameResolver.ServiceFactories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huvermann.Extension.DiNameResolving
{
    public class ServiceByNameFactory<TService> : NamedServiceProvider, IServiceByNameFactory<TService>
    {
        public ServiceByNameFactory(IServiceProvider serviceProvider, INameRegistrationService nameRegistrationService) : base(serviceProvider, nameRegistrationService)
        {
        }

        public TService GetServiceByName(string interfaceKey)
        {
            return GetServiceByName<TService>(interfaceKey);
        }
    }
}
