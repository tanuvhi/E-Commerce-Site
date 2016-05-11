[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UltraShopBd.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(UltraShopBd.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace UltraShopBd.WebUI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using UltraShopBd.WebUI.Controllers;
    using UltraShopBd.Domain.Concrete;
    using UltraShopBd.Domain.Abstract;
    using UltraShopBd.Domain.UShopConcrete;
    using UltraShopBd.Domain.UShopAbstract;
  

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUProductRepository>().To<UltraProductRepository>();
            kernel.Bind<IProductRepository>().To<EFProductRepositoy>();
           kernel.Bind<IUserRepository>().To<EFUserRepository>();
            kernel.Bind<IAuthentication>().To<FormsAuthenticationProvider>();
            kernel.Bind<IOrderRepository>().To<EFOrderRepository>();
            
          
        }   
    }
}
