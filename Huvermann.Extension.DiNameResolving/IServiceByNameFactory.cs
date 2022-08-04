using System;
using System.Collections.Generic;
using System.Text;

namespace Huvermann.Extension.DiNameResolving
{
    public interface IServiceByNameFactory<TService>
    {
        TService GetServiceByName(string interfaceKey);
    }
}
