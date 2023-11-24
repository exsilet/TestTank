using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class State
    {
        public int Difficult;
        public int CurrentHP;
        public int MaxHP;
        [Range(0,1)]
        public float Protection;
        public void ResetHP() => CurrentHP = MaxHP;

        public State()
        {
            Difficult = 1;
            CurrentHP = 10;
            MaxHP = 10;
            Protection = 0.2f;
        }
    }
}