using System.Linq;
using Infrastructure.Factory;
using Infrastructure.Service;
using Logic;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _enemyAnimator;

        private Transform _heroTransform;
        private Collider[] _hits = new Collider[1];
        private int _layerMask;
        private float _attackCooldown;
        private bool _isAttacking;
        private bool _attackIsActive;
        private IGameFactory _gameFactory;

        private float AttackCooldown => _attackCooldown;
        public int Damage { get; set; }
        public float Cleavage { get; set; }
        public float EffectiveDistance { get; set; }

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _gameFactory.HeroCreated += OnHeroCreated;
            _layerMask = 1 << LayerMask.NameToLayer("Hero");
        }

        private void OnHeroCreated() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private void Update()
        {
            UpdateCooldown();

            if(CanAttack())
                StartAttack();
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        private void OnAttackEnded()
        {
            _attackCooldown = AttackCooldown;
            _isAttacking = false;
        }

        public void DisableAttack() => 
            _attackIsActive = false;

        public void EnableAttack() => 
            _attackIsActive = true;

        private bool CooldownIsUp() => 
            _attackCooldown <= 0f;

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        private bool Hit(out Collider hit)
        {
            var hitAmount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();
      
            return hitAmount > 0;
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
                   transform.forward * EffectiveDistance;
        }

        private bool CanAttack() => 
            _attackIsActive && !_isAttacking && CooldownIsUp();

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _enemyAnimator.PlayAttack();
            _isAttacking = true;
        }
    }
}