using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;

namespace WeaponScripts
{
    public class PlayerWeaponHandler : MonoBehaviour
    {
        private PlayerInputHandler m_InputHandler;
        private WeaponManager wm;
        public Rigidbody projectile;
        public float BulletSpeed;

        void Start()
        {
            m_InputHandler = GetComponent<PlayerInputHandler>();
            wm = GetComponent<WeaponManager>();
        }
        void Update()
        {
            //bool for later actions (recoil, animations)
            bool hasFired = wm.HandleShootInputs(m_InputHandler.GetFireInputDown(), m_InputHandler.GetFireInputHeld(), m_InputHandler.GetFireInputReleased());
        }
    }
}
