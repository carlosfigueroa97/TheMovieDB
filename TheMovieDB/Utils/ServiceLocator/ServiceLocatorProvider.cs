using System;
namespace TheMovieDB.Utils.ServiceLocator
{
    public delegate IServiceLocator ServiceLocatorProviderDelegate();

    public class ServiceLocatorProvider
    {
        public static readonly Lazy<ServiceLocatorProvider> instance = new Lazy<ServiceLocatorProvider>(() =>
        {
            return new ServiceLocatorProvider();
        });

        public ServiceLocatorProvider(){}

        public static ServiceLocatorProvider Instance => instance.Value;

        public static ServiceLocatorProviderDelegate ServiceLocatorProviderDelegate = GetAutofacServiceLocator;

        public static IServiceLocator GetAutofacServiceLocator()
        {
            return AutofacServiceLocator.Instance;
        }

        public IServiceLocator Current
        {
            get
            {
                ServiceLocatorProviderDelegate funcImplementation = ServiceLocatorProviderDelegate;
                IServiceLocator implementation = funcImplementation?.Invoke();
                return implementation;
            }
        }

        public TInterface GetService<TInterface>() => Current.Resolve<TInterface>();
    }
}
