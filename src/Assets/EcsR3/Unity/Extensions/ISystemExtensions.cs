using System;
using SystemsR3.Systems;
using R3;

namespace EcsR3.Unity.Extensions
{
    public static class ISystemExtensions
    {
        public static Observable<Unit> WaitForScene(this ISystem system)
        { return Observable.EveryUpdate().Take(1); }
        
        public static void AfterUpdateDo(this ISystem system, Action action)
        { WaitForScene(system).Subscribe(_ => action()); }
    }
}