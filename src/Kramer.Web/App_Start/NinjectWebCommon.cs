using System.Configuration;
using Kramer.Helpers;
using Kramer.Repository;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Kramer.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Kramer.App_Start.NinjectWebCommon), "Stop")]

namespace Kramer.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Kramer.Models;
    using Kramer.Repository.Interfaces;
    using Kramer.Services;

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
            //Isso aqui quer dizer que para cada requisição que for feita, ele vai criar uma instância do DbContext.
            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
            kernel.Bind<IUserRequestRepository>().To<UserRequestRepository>().InRequestScope();
            kernel.Bind<ISaleTypeRepository>().To<SaleTypeRepository>().InRequestScope();
            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            kernel.Bind<IRoleRepository>().To<RoleRepository>().InRequestScope();
            kernel.Bind<ISaleTypeService>().To<SaleTypeService>().InRequestScope();
            kernel.Bind<IStatusRepository>().To<StatusRepository>().InRequestScope();
            kernel.Bind<IEmailSender>().To<EmailSender>().InRequestScope()
                .WithConstructorArgument("host", ConfigurationManager.AppSettings["SmtpHost"])
                .WithConstructorArgument("port", int.Parse(ConfigurationManager.AppSettings["SmtpPort"]))
                .WithConstructorArgument("username", ConfigurationManager.AppSettings["SmtpUsername"])
                .WithConstructorArgument("password", ConfigurationManager.AppSettings["SmtpPassword"])
                .WithConstructorArgument("enableSsl", bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"]))
                .WithConstructorArgument("throwError", bool.Parse(ConfigurationManager.AppSettings["SmtpThrowError"]));

        }        
    }
}
