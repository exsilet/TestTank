using UnityEngine;

namespace Player
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMover _move;
        [SerializeField] private HeroAttack _attack;
        
        private bool _isDead;
        
        private void Start()
        {
            _health.HealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (!_isDead && _health.Current <= 0) 
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _move.enabled = false;
            _attack.enabled = false;
        }
    }
}