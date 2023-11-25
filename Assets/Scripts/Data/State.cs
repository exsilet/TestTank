using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class State
    {
        public int Difficult;
        public float CurrentHP;
        public float MaxHP;
        [Range(0,1)]
        public float Protection;
        public void ResetHP() => CurrentHP = MaxHP;

        public State()
        {
            Difficult = 1;
            CurrentHP = 100;
            MaxHP = 100;
            Protection = 0.2f;
        }
    }
}