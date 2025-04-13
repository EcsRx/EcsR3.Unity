using System;
using R3;
using SystemsR3.Scheduling;
using UnityEngine;

namespace EcsR3.Unity.Scheduling
{
    public class UnityUpdateScheduler : IUpdateScheduler
    {
        public ElapsedTime ElapsedTime { get; private set; }
        
        private readonly Subject<ElapsedTime> _onPreUpdate = new Subject<ElapsedTime>();
        private readonly Subject<ElapsedTime> _onUpdate = new Subject<ElapsedTime>();
        private readonly Subject<ElapsedTime> _onPostUpdate = new Subject<ElapsedTime>();
        private readonly IDisposable _everyUpdateSub;
        
        public Observable<ElapsedTime> OnPreUpdate => _onPreUpdate;
        public Observable<ElapsedTime> OnUpdate => _onUpdate;
        public Observable<ElapsedTime> OnPostUpdate => _onPostUpdate;

        public UnityUpdateScheduler()
        {
            _everyUpdateSub = Observable.EveryUpdate().Subscribe(x =>
            {
                var deltaTime = TimeSpan.FromSeconds(Time.deltaTime);
                var totalTime = ElapsedTime.TotalTime + deltaTime;
                ElapsedTime = new ElapsedTime(deltaTime, totalTime);
                
                _onPreUpdate?.OnNext(ElapsedTime);
                _onUpdate?.OnNext(ElapsedTime);
                _onPostUpdate.OnNext(ElapsedTime);
            });
        }

        public void Dispose()
        {
            _everyUpdateSub.Dispose();
            _onPreUpdate?.Dispose();
            _onPostUpdate?.Dispose();
            _onUpdate?.Dispose();
        }
    }
}