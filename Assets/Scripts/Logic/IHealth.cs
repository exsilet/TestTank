using System;

namespace Logic
{
    public interface IHealth
    {
        event Action HealthChanged;
        float Current { get; set; }
        float Max { get; set; }
        float Protection { get; set; }
        void TakeDamage(int damage);
    }
}