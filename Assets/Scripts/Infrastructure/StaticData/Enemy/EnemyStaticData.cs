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
        public float MoveSpeed;
        public int Reward;
        public float Cleavage;
        public float EffectiveDistance;
        
        public GameObject Prefab;
        public MonsterTypeID monsterTypeID;
    }
}

