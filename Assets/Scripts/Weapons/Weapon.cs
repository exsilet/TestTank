﻿using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Bullet Bullets;

        public abstract void Shoot(Transform shootPoint);
    }
}