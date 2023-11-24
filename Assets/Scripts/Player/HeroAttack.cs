using System.Collections.Generic;
using Infrastructure.Service;
using UnityEngine;
using Weapons;

namespace Player
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private List<Weapon> _weapons;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _miniShootPoint;

        private IInputService _input;
        private int _currentWeaponNumber = 0;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            if (_input.IsAttackButtonX())
            {
                Hit();
            }

            if (Input.GetKey(KeyCode.Q))
            {
                NextWeapon();
            }
            else if (Input.GetKey(KeyCode.W))
            {
                PreviousWeapon();
            }
        }

        private void Hit()
        {
            if (_currentWeaponNumber == 0)
            {
                _weapons[_currentWeaponNumber].Shoot(_shootPoint);
            }
            else if (_currentWeaponNumber == 1)
            {
                _weapons[_currentWeaponNumber].Shoot(_miniShootPoint);
            }
            else
            {
                Debug.Log("no weapon");
            }
        }

        private void NextWeapon()
        {
            if (_currentWeaponNumber == _weapons.Count-1)
                _currentWeaponNumber = 0;

            ChangeWeapon(_weapons[_currentWeaponNumber]);
        }

        private void PreviousWeapon()
        {
            if (_currentWeaponNumber == 0)
                _currentWeaponNumber = _weapons.Count- 1;

            ChangeWeapon(_weapons[_currentWeaponNumber]);
        }

        private void ChangeWeapon(Weapon weapon)
        {
            if (_weapons[0] == null )
            {
                _weapons[0] = (Pistol)weapon;

            }
            else if (_weapons[1] == null)
            {
                _weapons[1] = (Rocket)weapon;
            }
        }
    }
}