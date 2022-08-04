using System;
using System.Collections.Generic;
using System.Text;

namespace Huvermann.Extension.NameResolver.ServiceFactories
{
    public interface INameRegistrationService
    {
        Dictionary<string, Dictionary<string, Func<IServiceProvider, object>>> NameRegistry { get; }
    }
}
