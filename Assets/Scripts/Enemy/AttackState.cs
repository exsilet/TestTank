using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu]
    public class AttackState : State
    {
        [SerializeField] private float _delay;

        private float _lastAttackTime;
        private EnemyAnimator _enemyAnimator;
        private bool _onShoot;

        public override void Init()
        {
            _enemyAnimator = Enemy.GetComponent<EnemyAnimator>();
        }
        
        public override void Run()
        {
            if(IsFinished)
                return;

            OnAttack();
        }

        private void OnAttack()
        {
            if (_lastAttackTime <= 0)
            {
                _enemyAnimator.PlayAttack();
                _enemyAnimator.PlayAttackButtonUp();
                _lastAttackTime = _delay;
            }

            Enemy.Shoot = true;
            _lastAttackTime -= Time.deltaTime;

            IsFinished = true;
        }
        
        //таймер остановки
    }
}