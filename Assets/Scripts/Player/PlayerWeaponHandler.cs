using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;
using System;

namespace WeaponScripts
{
    public class PlayerWeaponHandler : MonoBehaviour
    {
        private PlayerInputHandler m_InputHandler;
        private WeaponManager wm;
        public Rigidbody projectile;
        public float BulletSpeed;


        [Header("Current Gun")]
        public GameObject Gun;
        [Header("gun positions")]
        public Transform DownPos;
        public Transform HipPos;
        public Transform AimPos;
        //public Vector3 newPosition;

        float maxDistanceSquared = 0.0000001f;
        StateData oldState;
        void Start()
        {
            m_InputHandler = GetComponent<PlayerInputHandler>();
            wm = GetComponent<WeaponManager>();
            PlayerStateThird.StateChange += onStateChange;
        }


        void Update()
        {
            //bool for later actions (recoil, animations)
            bool hasFired = wm.HandleShootInputs(m_InputHandler.GetFireInputDown(), m_InputHandler.GetFireInputHeld(), m_InputHandler.GetFireInputReleased());
        }
        private void onStateChange(StateData newStateData)
        {
            StopCoroutine("GunMovement");
            StartCoroutine("GunMovement", newStateData.WeaponPosition);
            StopCoroutine("GunRotation");
            StartCoroutine("GunRotation", newStateData.WeaponRotation);
        }
        private IEnumerator GunMovement(Vector3 newPosition)
        {
            while ((newPosition - Gun.transform.localPosition).sqrMagnitude > maxDistanceSquared)
            {
                //movetoward newposition
                Gun.transform.localPosition = Vector3.MoveTowards(Gun.transform.localPosition, newPosition, 1f * Time.deltaTime);
                yield return null;
            }
            Gun.transform.localPosition = newPosition;
        }
        private IEnumerator GunRotation(Vector3 newRotation)
        {
            Gun.transform.localRotation = Quaternion.Euler(newRotation);
            yield return null;
            //should learn eventually but feels pointless for a .25 second animation at this point
            /*
            while ((newRotation != Gun.transform.localRotation.eulerAngles)) // - Gun.transform.localRotation.eulerAngles).sqrMagnitude > maxDistanceSquared)
            {
                Gun.transform.localRotation = Quaternion.Euler(Vector3.RotateTowards(Gun.transform.localRotation.eulerAngles, newRotation, 0, 0));
                yield return null;
            }
            */
        }
    }
}
