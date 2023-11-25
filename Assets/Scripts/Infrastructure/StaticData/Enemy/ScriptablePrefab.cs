using Enemy;
using UnityEngine;

namespace Infrastructure.StaticData.Enemy
{
    [CreateAssetMenu(fileName = "EnemyType", menuName = "EnemyType")]
    public class ScriptablePrefab : ScriptableObject
    {
        [SerializeField] private EnemyAttack[] _prefabs;

        private EnemyAttack _enemyPrefab;

        public EnemyAttack GetRandomPrefab()
        {
            int randomPrefab = Random.Range(0, _prefabs.Length);
            _enemyPrefab = _prefabs[randomPrefab];
            return _enemyPrefab;
        }
    }
}