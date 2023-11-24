using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu]
    public class RandomMoveState : State
    {
        [SerializeField] private float _minPositionX;
        [SerializeField] private float _maxPositionX;
        [SerializeField] private float _minPositionZ;
        [SerializeField] private float _maxPositionZ;
        
        public float MaxDistance = 5f;
        private Vector3 _randomPosition;

        public override void Init()
        {
            var randomed = new Vector3(Random.Range(_minPositionX, _maxPositionX), 0f,
                Random.Range(_minPositionZ, _maxPositionZ));
            _randomPosition = Enemy.transform.position + randomed;
            //навмесш
        }

        public override void Run()
        {
            var distance = (_randomPosition - Enemy.transform.position).magnitude;
            Enemy.Shoot = false;

            if (distance > 0.5f)
                Enemy.MoveTo(_randomPosition);
            else
                IsFinished = true;
        }
    }
}