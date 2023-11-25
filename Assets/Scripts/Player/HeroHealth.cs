using System;
using Data;
using Infrastructure.Service.SaveLoad;
using Logic;
using UnityEngine;

namespace Player
{
    public class HeroHealth : MonoBehaviour, IHealth, ISavedProgress
    {
        private State _state;

        public event Action HealthChanged;

        public float Current
        {
            get => _state.CurrentHP;
            set
            {
                if ((int)value != _state.CurrentHP)
                {
                    _state.CurrentHP = (int)value;

                    HealthChanged?.Invoke();
                }
            }
        }

        public float Protection
        {
            get => _state.Protection;
            set => _state.Protection = value;
        }

        public float Max
        {
            get => _state.MaxHP;
            set => _state.MaxHP = (int)value;
        }

        public void TakeDamage(int damage)
        {
            if (Current <= 0)
                return;
            
            Current -= damage * Protection;
        }
        

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHP = Current;
            progress.HeroState.MaxHP = Max;
        }
    }
}