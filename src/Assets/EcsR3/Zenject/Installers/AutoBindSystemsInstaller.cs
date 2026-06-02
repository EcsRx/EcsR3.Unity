using System.Collections.Generic;
using EcsR3.Zenject.Helpers;
using Zenject;

namespace EcsR3.Zenject.Installers
{
    /// <summary>
    /// This is for just binding systems and not registering them
    /// </summary>
    public class AutoBindSystemsInstaller : MonoInstaller
    {
        public List<string> SystemNamespaces = new List<string>();
        
        public override void InstallBindings()
        {
            BindSystemsInNamespace.Bind(Container, SystemNamespaces);
        }
    }
}