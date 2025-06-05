using System.Collections.Generic;
using System.Linq;
using EcsR3.Collections.Entities;
using EcsR3.Components.Database;
using EcsR3.Computeds.Entities.Registries;
using EcsR3.Entities.Accessors;
using EcsR3.Infrastructure;
using EcsR3.Infrastructure.Modules;
using EcsR3.Plugins.Views;
using EcsR3.Unity.Modules;
using SystemsR3.Events;
using SystemsR3.Executor;
using SystemsR3.Extensions;
using SystemsR3.Infrastructure.Dependencies;
using SystemsR3.Infrastructure.Extensions;
using SystemsR3.Infrastructure.Modules;
using SystemsR3.Infrastructure.Plugins;
using SystemsR3.Systems;
using UnityEngine;

namespace EcsR3.Unity
{
    [DefaultExecutionOrder(-20000)]
    public abstract class UnityEcsR3ApplicationBehaviour : MonoBehaviour, IEcsR3Application
    {
        public IDependencyRegistry DependencyRegistry { get; protected set; }
        public IDependencyResolver DependencyResolver { get; protected set; }
        
        public ISystemExecutor SystemExecutor { get; private set; }
        public IEventSystem EventSystem { get; private set; }
        public IEntityCollection EntityCollection { get; private set; }
        public IEntityComponentAccessor EntityComponentAccessor { get; private set; }
        public IComponentDatabase ComponentDatabase { get; private set; }
        public IComputedEntityGroupRegistry ComputedEntityGroupRegistry { get; private set; }
        public IEnumerable<ISystemsR3Plugin> Plugins => _plugins;
        
        protected List<ISystemsR3Plugin> _plugins { get; } = new List<ISystemsR3Plugin>();

        protected abstract void ApplicationStarted();
        
        public virtual void StartApplication()
        {
            LoadModules();
            LoadPlugins();
            SetupPlugins();
            BindSystems();

            DependencyResolver = DependencyRegistry.BuildResolver();
            ResolveApplicationDependencies();
            StartPluginSystems();
            StartSystems();
            ApplicationStarted();
        }
        
        public virtual void StopApplication()
        { StopAllSystems(); }

        /// <summary>
        /// Load any plugins that your application needs
        /// </summary>
        /// <remarks>It is recommended you just call RegisterPlugin method in here for each plugin you need</remarks>
        protected virtual void LoadPlugins()
        {
            RegisterPlugin(new ViewsPlugin());
        }

        /// <summary>
        /// Load any modules that your application needs
        /// </summary>
        /// <remarks>
        /// If you wish to use the default setup call through to base, if you wish to stop the default framework
        /// modules loading then do not call base and register your own internal framework module.
        /// </remarks>
        protected virtual void LoadModules()
        {
            DependencyRegistry.LoadModule<FrameworkModule>();
            DependencyRegistry.LoadModule( new EcsR3InfrastructureModule()
                { ComponentDatabaseConfig = OverrideComponentDatabaseConfig() });
            DependencyRegistry.LoadModule<UnityOverrideModule>();
        }
        
        /// <summary>
        /// Resolve any dependencies the application needs
        /// </summary>
        /// <remarks>By default it will setup SystemExecutor, EventSystem, EntityCollectionManager</remarks>
        protected virtual void ResolveApplicationDependencies()
        {
            SystemExecutor = DependencyResolver.Resolve<ISystemExecutor>();
            EventSystem = DependencyResolver.Resolve<IEventSystem>();
            EntityCollection = DependencyResolver.Resolve<IEntityCollection>();
            ComponentDatabase = DependencyResolver.Resolve<IComponentDatabase>();
            ComputedEntityGroupRegistry = DependencyResolver.Resolve<IComputedEntityGroupRegistry>();
            EntityComponentAccessor = DependencyResolver.Resolve<IEntityComponentAccessor>();
        }
        
        /// <summary>
        /// Allows you to override the default component pool database settings, which can hugely reduce allocations
        /// and startup speed, it is entirely optional and defaults are used if nothing is overidden.
        /// </summary>
        /// <returns>A component database configuration to use</returns>
        public virtual ComponentDatabaseConfig OverrideComponentDatabaseConfig() => new ComponentDatabaseConfig() {};
        
        /// <summary>
        /// Bind any systems that the application will need
        /// </summary>
        /// <remarks>By default will auto bind any systems within application scope</remarks>
        protected virtual void BindSystems()
        { this.BindAllSystemsWithinApplicationScope(); }
        
        protected virtual void StopAllSystems()
        {
            var allSystems = SystemExecutor.Systems.ToList();
            allSystems.ForEachRun(SystemExecutor.RemoveSystem);
            DependencyRegistry.Unbind<ISystem>();
        }

        /// <summary>
        /// Start any systems that the application will need
        /// </summary>
        /// <remarks>By default it will auto start any systems which have been bound</remarks>
        protected virtual void StartSystems()
        { this.StartAllBoundSystems(); }
        
        protected virtual void SetupPlugins()
        { _plugins.ForEachRun(x => x.SetupDependencies(DependencyRegistry)); }

        protected virtual void StartPluginSystems()
        {
            _plugins.SelectMany(x => x.GetSystemsForRegistration(DependencyResolver))
                .ForEachRun(x => SystemExecutor.AddSystem(x));
        }

        protected void RegisterPlugin(ISystemsR3Plugin plugin)
        { _plugins.Add(plugin); }
    }
}