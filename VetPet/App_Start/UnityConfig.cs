using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.AspNet.Identity;
using VetPet.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using VetPet.Controllers;

namespace VetPet.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType(
                typeof(Infrastructure.Data.UnitOfWork.MainBCUnitOfWork),
                new PerResolveLifetimeManager());

            container.RegisterType<
                Infrastructure.Data.Seedwork.IQueryableUnitOfWork,
                Infrastructure.Data.UnitOfWork.MainBCUnitOfWork>();

            container.RegisterType<DbContext, ApplicationDbContext>(
            new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(
                new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new HierarchicalLifetimeManager());

            container.RegisterType<AccountController>(
                new InjectionConstructor());
            container.RegisterType<ManageController>(
                new InjectionConstructor());

            container.RegisterType<
                Application.PetModule.Services.IPetAppService
                , Application.PetModule.Services.PetAppService>();
            container.RegisterType<
                Application.PetModule.Services.ITaskAppService
                , Application.PetModule.Services.TaskAppService>();

            container.RegisterType<
                Domain.PetModule.Aggregate.PetAgg.IPetRepository,
                Infrastructure.Data.PetModule.PetRepository>();
            container.RegisterType<
                Domain.PetModule.Aggregate.TaskAgg.ITaskRepository,
                Infrastructure.Data.PetModule.TaskRepository>();
        }
    }
}
