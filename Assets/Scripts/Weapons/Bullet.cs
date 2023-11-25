using System.Collections;
using Enemy;
using UnityEngine;

namespace Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private float _lifeTime;

        private void Update()
        {
            transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
            StartCoroutine(LifeTime());
        }
        
        private IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(_lifeTime);
            Destroy(gameObject);
            yield break;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other);
            
            if (other.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}