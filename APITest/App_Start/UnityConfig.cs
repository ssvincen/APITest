using APITest.BI;
using APITest.Providers;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace APITest
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IFlickrImagesDataAccess, FlickrImagesDataAccess>(); 
            container.RegisterType<IConnectionManager, ConnectionManager>(); 

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}