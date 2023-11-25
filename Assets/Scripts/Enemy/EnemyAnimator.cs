using System;
using Logic;
using UnityEngine;

namespace Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] public Animator _animator;

        private static readonly int MoveHash = Animator.StringToHash("Run");
        private static readonly int AttackHash = Animator.StringToHash("BasicAttack");
        private static readonly int HitHash = Animator.StringToHash("Hit");
        private static readonly int DieHash = Animator.StringToHash("Die");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _attackStateHash = Animator.StringToHash("Basic Attack");
        private readonly int _walkingStateHash = Animator.StringToHash("Run");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        public bool IsAttacking => State == AnimatorState.Attack;
        public void PlayHit() => _animator.SetTrigger(HitHash);
        public void PlayDeath() => _animator.SetTrigger(DieHash);
        public void PlayAttack() => _animator.SetTrigger(AttackHash);

        public void Move()
        {
            _animator.SetTrigger(MoveHash);
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash)
        {
            StateExited?.Invoke(StateFor(stateHash));
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
            {
                state = AnimatorState.Idle;
            }
            else if (stateHash == _attackStateHash)
            {
                state = AnimatorState.Attack;
            }
            else if (stateHash == _walkingStateHash)
            {
                state = AnimatorState.Walking;
            }
            else
            {
                state = AnimatorState.Unknown;
            }

            return state;
        }
    }
}