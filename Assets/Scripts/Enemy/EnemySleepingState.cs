using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu]
    public class EnemySleepingState : State
    {
        [SerializeField] private float _sleepTimeLeft = 5f;

        private bool _isSleepingStarted;

        public override void Run()
        {
            if(IsFinished)
                return;

            if (_isSleepingStarted)
                DoSleep();
        }

        private void DoSleep()
        {
            _sleepTimeLeft -= Time.deltaTime;
            
            if(_sleepTimeLeft > 0)
                return;

            IsFinished = true;
        }
    }
}