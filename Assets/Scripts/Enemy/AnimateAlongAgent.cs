using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float MinimalVelocity = 0.1f;
    
        [SerializeField] private NavMeshAgent Agent;
        [SerializeField] private EnemyAnimator _animator;

        private void Update()
        {
            if(ShouldMove())
                _animator.Move();
        }

        private bool ShouldMove() => 
            Agent.velocity.magnitude > MinimalVelocity && Agent.remainingDistance > Agent.radius;
    }
}