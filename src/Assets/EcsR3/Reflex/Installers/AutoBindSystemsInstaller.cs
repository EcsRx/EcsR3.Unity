using System.Collections.Generic;
using EcsR3.Reflex.Helpers;
using Reflex.Core;
using UnityEngine;

namespace EcsR3.Reflex.Installers
{
    public class AutoBindSystemsInstaller : MonoBehaviour, IInstaller
    {
        public List<string> SystemNamespaces = new List<string>();

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            BindSystemsInNamespace.Bind(containerBuilder, SystemNamespaces);
        }
    }
}