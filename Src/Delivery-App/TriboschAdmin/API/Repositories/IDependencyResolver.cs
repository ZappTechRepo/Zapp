using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriboschAdmin.API.Repositories
{
    public interface IDependencyResolver : IDependencyScope, IDisposable
    {
        IDependencyScope BeginScope();
    }

    public interface IDependencyScope : IDisposable
    {
        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);

    }
}