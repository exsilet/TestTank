﻿using UnityEngine;

namespace Enemy
{
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
      
            _attack.DisableAttack();
        }

        private void TriggerExit(Collider obj)
        {
            _attack.DisableAttack();
        }

        private void TriggerEnter(Collider obj)
        {
            _attack.EnableAttack();
        }
    }
}