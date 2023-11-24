using UnityEngine;

namespace Weapons
{
    public class Rocket : Weapon
    {
        public override void Shoot(Transform shootPoint)
        {
            Instantiate(Bullets, shootPoint.position, shootPoint.rotation);
        }
    }
}