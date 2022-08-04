using System;
using System.Collections.Generic;
using System.Text;

namespace Huvermann.Extension.NameResolver.ServiceFactories
{
    public class NamedServiceProvider : INamedServiceProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INameRegistrationService _nameRegistrationService;

        public NamedServiceProvider(IServiceProvider serviceProvider, INameRegistrationService nameRegistrationService)
        {
            _serviceProvider = serviceProvider;
            _nameRegistrationService = nameRegistrationService;
        }
        public TService GetServiceByName<TService>(string interfaceKey)
        {
            string servicename = typeof(TService).FullName;
            if (!_nameRegistrationService.NameRegistry.ContainsKey(servicename))
            {
                throw new InvalidOperationException($"Service not registered! : {servicename}");
            }
            var interfaceRegistry = _nameRegistrationService.NameRegistry[servicename];
            if (!interfaceRegistry.ContainsKey(interfaceKey))
            {
                throw new InvalidOperationException($"No Service registered for key: {interfaceKey}");
            }
            var instantiation = interfaceRegistry[interfaceKey];
            return (TService)instantiation(_serviceProvider);
        }
    }
}
