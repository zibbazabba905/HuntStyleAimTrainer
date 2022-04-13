using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponScripts
{

    //will contain data about weapon
    //I want this to contain all the weapon basics, bullet speed, sway, crosshairs, ammo
    public class GunScript : MonoBehaviour
    {
        public Rigidbody Projectile;
        public float BulletSpeed;
        public float Delay;
    }
}