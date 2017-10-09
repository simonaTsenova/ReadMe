[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ReadMe.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ReadMe.Web.App_Start.NinjectWebCommon), "Stop")]

namespace ReadMe.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using ReadMe.Data.Contracts;
    using ReadMe.Data;
    using System.Data.Entity;
    using ReadMe.Authentication.Contracts;
    using ReadMe.Authentication;
    using ReadMe.Providers.Contracts;
    using ReadMe.Providers;
    using ReadMe.Factories;
    using Ninject.Extensions.Factory;
    using Ninject.Extensions.Conventions;
    using ReadMe.Web.Infrastructure.Factories;
    using ReadMe.Services.Contracts;
    using ReadMe.Services;
    using AutoMapper;

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
            kernel.Bind(x =>
            {
                x.FromThisAssembly()
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            kernel.Bind<IAuthenticationProvider>().To<AuthenticationProvider>().InSingletonScope();
            kernel.Bind<IDateTimeProvider>().To<DateTimeProvider>().InSingletonScope();
            kernel.Bind<IHttpContextProvider>().To<HttpContextProvider>().InSingletonScope();

            kernel.Bind<IAuthorFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IBookFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IGenreFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IPublisherFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IRatingFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IReviewFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IUserFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IUserBookFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IViewModelFactory>().ToFactory().InSingletonScope();

            kernel.Bind<IAuthorService>().To<AuthorService>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IBookService>().To<BookService>().InRequestScope();
            kernel.Bind<IGenreService>().To<GenreService>().InRequestScope();
            kernel.Bind<IRatingService>().To<RatingService>().InRequestScope();
            kernel.Bind<IReviewService>().To<ReviewService>().InRequestScope();
            kernel.Bind<IPublisherService>().To<PublisherService>().InRequestScope();
            kernel.Bind<IUserBookService>().To<UserBookService>().InRequestScope();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind(typeof(IEfRepository<>)).To(typeof(EfRepository<>)).InRequestScope();
            kernel.Bind<ReadMeDbContext>().ToSelf().InRequestScope();
            kernel.Bind<IMapper>().ToMethod(x => Mapper.Instance).InSingletonScope();
        }
    }
}
