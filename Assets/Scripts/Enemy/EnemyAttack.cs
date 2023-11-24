using UnityEngine;
using Weapons;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyWeapon _weapon;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private EnemyAnimator _enemyAnimator;

        private bool _stand;

        private void OnAttack()
        {
            _weapon.Shoot(_shootPoint);
        }
    }
}