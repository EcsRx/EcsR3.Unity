using System;
using EcsR3.Components;
using R3;
using UnityEngine;

namespace EcsR3.Examples.RandomReactions.Components
{
    public class RandomColorComponent : IComponent, IDisposable
    {
        public ReactiveProperty<Color> Color { get; }
        public float Elapsed { get; set; }
        public float NextChangeIn { get; set; }

        public RandomColorComponent()
        {
            Color = new ReactiveProperty<Color>();
        }

        public void Dispose()
        { Color?.Dispose(); }
    }
}