using System;
using EcsR3.Examples.UsingBlueprints.Components;
using EcsR3.Groups;

namespace EcsR3.Examples.UsingBlueprints.Groups
{
    public class PlayerGroup : IGroup
    {
        public Type[] RequiredComponents { get; } = {
            typeof(HasName), typeof(WithHealthComponent)
        };

        public Type[] ExcludedComponents { get; } = new Type[0];
    }
}