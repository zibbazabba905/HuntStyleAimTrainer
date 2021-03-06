using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;


namespace WeaponScripts
{
    //probably YAGNI, will be a while before I add bespoke weapons if I do this method at all
    //mostly taken from a unity tutorial demo
    public class WeaponManager : MonoBehaviour
    {
        public Transform BulletSpawnPoint;

        float m_LastTimeShot = Mathf.NegativeInfinity;
        public float DelayBetweenShots = 0.5f;//FIX SET TO WEAPONSCRIPT
        public bool IsReloading { get; private set; }

        public GameObject ActiveWeapon;
        private GunScript GS;

        void Start()
        {
            IsReloading = false;
            GS = ActiveWeapon.GetComponent<GunScript>();
            DelayBetweenShots = GS.Delay;
        }

        public bool HandleShootInputs(bool inputDown, bool inputHeld, bool inputUp)
        {
            if (inputDown && PlayerStateThird.Instance.CurrentState != PlayerStateThird.MoveState.Down)
            {
                return TryShoot();
            }
            return false;
        }
        bool TryShoot()
        {
            if (m_LastTimeShot + DelayBetweenShots < Time.time)
            {
                HandleShoot();
                return true;
            }
            return false;
        }
        void HandleShoot()
        {
            Rigidbody clone;
            clone = Instantiate(GS.Projectile, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
            clone.velocity = BulletSpawnPoint.forward * GS.BulletSpeed;
            ScoreScripts.ScoreManager.Instance.ShotCounter();
            m_LastTimeShot = Time.time;
        }
    }
}