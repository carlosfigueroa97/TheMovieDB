using System;
using Autofac;

namespace TheMovieDB.Utils.ServiceLocator
{
    public class AutofacServiceLocator : IServiceLocator
    {
        #region Properties

        private IContainer Container { get; set; }

        private ContainerBuilder ContainerBuilder { get; set; }

        private static AutofacServiceLocator _instance;
        public static AutofacServiceLocator Instance => _instance ?? (_instance = new AutofacServiceLocator());

        #endregion

        public AutofacServiceLocator()
        {
            ContainerBuilder = new ContainerBuilder();
        }

        #region Public Methods

        public void Build()
        {
            if(Container != null)
            {
                return;
            }

            Container = ContainerBuilder.Build();
        }

        public void Init()
        {
            Build();
        }

        public void Register<T>() where T : class => ContainerBuilder.RegisterType<T>();

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface =>
            ContainerBuilder.RegisterType<TImplementation>().As<TInterface>();

        public void RegisterSingle<TInterface, TImplementation>() where TImplementation : class, TInterface =>
            ContainerBuilder.RegisterType<TImplementation>().As<TInterface>().SingleInstance();

        public void RegisterSingleInstace<TInterface, TImplementation>(TImplementation implementation) where TImplementation : class, TInterface =>
            ContainerBuilder.RegisterInstance(implementation).As<TInterface>().SingleInstance();

        public T Resolve<T>() => Container.Resolve<T>();

        public object Resolve(Type type)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                return scope.Resolve(type);
            }
        }

        #endregion
    }
}
