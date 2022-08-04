using Huvermann.Extension.NameResolver.ServiceFactories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huvermann.Extension.DiNameResolving
{
    public static class ServiceFactoriesExtension
    {
        public static IServiceCollection AddTransientByName<TService, TImplementation>(this IServiceCollection services, string interfaceKey)
          where TService : class
          where TImplementation : class, TService
        {
            services.AddTransient<TService, TImplementation>();
            services.AddTransient<TImplementation>();
            string servicename = typeof(TService).FullName;
            var NameDict = new NameRegistrationService();


            var serviceReg = NameDict.NameRegistry.ContainsKey(servicename) == true ? NameDict.NameRegistry[servicename] : null;
            if (serviceReg == null)
            {
                serviceReg = new Dictionary<string, Func<IServiceProvider, object>>();
            }
            NameDict.NameRegistry[servicename] = serviceReg;

            Func<IServiceProvider, TImplementation> call = new Func<IServiceProvider, TImplementation>(serviceProvider =>
            {
                return (TImplementation)serviceProvider.GetService(typeof(TImplementation));
            });

            serviceReg[interfaceKey] = call;
            return services;
        }

        public static IServiceCollection RemoveAllNameRegs(this IServiceCollection services)
        {
            var NameDict = new NameRegistrationService();
            NameDict.NameRegistry.Clear();
            return services;
        }

    }
}
