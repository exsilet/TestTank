using UnityEngine;

namespace Weapons
{
    public class MachineGun : Weapon
    {
        public override void Shoot(Transform shootPoint)
        {
            Instantiate(Bullets, shootPoint.position, shootPoint.rotation);
        }
    }
}