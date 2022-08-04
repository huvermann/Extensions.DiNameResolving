using Huvermann.Extension.NameResolver.ServiceFactories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huvermann.Extension.DiNameResolving
{
    public static class Registration
    {
        public static IServiceCollection AddNamedServiceRegistration(this IServiceCollection services)
        {
            services.AddTransient<INameRegistrationService, NameRegistrationService>();
            services.AddTransient<INamedServiceProvider, NamedServiceProvider>();
            return services;
        }
    }
}
