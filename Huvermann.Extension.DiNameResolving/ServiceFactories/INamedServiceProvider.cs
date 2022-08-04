using System;
using System.Collections.Generic;
using System.Text;

namespace Huvermann.Extension.NameResolver.ServiceFactories
{
    public interface INamedServiceProvider
    {
        TService GetServiceByName<TService>(string interfaceKey);
    }
}
