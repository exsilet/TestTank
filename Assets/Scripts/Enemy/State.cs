using UnityEngine;

namespace Enemy
{
    public abstract class State : ScriptableObject
    {
        [HideInInspector] public EnemyMonkey Enemy;
        public bool IsFinished { get; protected set; }

        public virtual void Init() { }

        public abstract void Run();
    }
}