using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerInputHandler : MonoBehaviour
    {
        //HOW MANY OF THESE DO I NEED?
        [Tooltip("Sensitivity multiplier for moving the camera around")]
        public float LookSensitivity = 1f;

        [Tooltip("Used to flip the vertical input axis")]
        public bool InvertYAxis = false;

        [Tooltip("Used to flip the horizontal input axis")]
        public bool InvertXAxis = false;

        bool m_FireInputWasHeld;
        bool m_EscapeInputHappened;

        void Start()
        {
            //send to Menus
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }

        void LateUpdate()
        {
            m_FireInputWasHeld = GetFireInputHeld();
        }
        public bool GetFireInputDown()
        {
            return GetFireInputHeld() && !m_FireInputWasHeld;
        }
        public bool GetFireInputReleased()
        {
            return !GetFireInputHeld() && m_FireInputWasHeld;
        }
        public bool GetFireInputHeld()
        {
            if (CanProcessInput())
            {
                return Input.GetButton("Fire1");
            }
            return false;
        }

        public bool CanProcessInput()
        {
            return Cursor.lockState == CursorLockMode.Locked && GameManager.Instance.IsGameRunning;
        }

        public Vector3 GetMoveInput()
        {
            if (CanProcessInput())
            {
                Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
                move = Vector3.ClampMagnitude(move, 1);//normalizes movement
                return move;
            }
            return Vector3.zero;
        }
        public float GetLookInputsHorizontal()
        {
            return CanProcessInput()? InvertXAxis ? -Input.GetAxisRaw("Mouse X") : Input.GetAxisRaw("Mouse X") : 0f;
        }
        public float GetLookInputsVertical()
        {
            return CanProcessInput()? InvertYAxis ? -Input.GetAxisRaw("Mouse Y") : Input.GetAxisRaw("Mouse Y") : 0f;
        }
    }
}
