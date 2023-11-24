using UnityEngine;

namespace Infrastructure.StaticData.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Monster")]
    public class EnemyStaticData : ScriptableObject
    {
        public float Hp;
        public int Damage;
        [Range(0,1)] public float protection;
        public float Cooldown;
        public float Speed;
        public int Reward;
        
        public GameObject Prefab;
        public EnemyTypeID EnemyTypeID;
    }
}

