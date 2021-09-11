using System;
namespace TheMovieDB.Utils.ServiceLocator
{
    public interface IServiceLocator
    {
        T Resolve<T>();

        void Init();

        object Resolve(Type type);

        void Register<T>() where T : class;

        void Register<TInterface, TImplementation>() where TImplementation : TInterface;

        void RegisterSingle<TInterface, TImplementation>() where TImplementation : class, TInterface;

        void RegisterSingleInstace<TInterface, TImplementation>(TImplementation implementation) where TImplementation : class, TInterface;
    }
}
