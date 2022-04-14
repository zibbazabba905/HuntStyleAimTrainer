using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;
using System;
//using Patterns;

namespace PlayerScripts
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInputHandler))]
    public class PlayerCharacterController : MonoBehaviour
    {


        //using camera "rig" instead of camera to hold objects seperate from camera view
        public GameObject Rig;
        public Camera Camera;

        //no jump yet
        public float GravityDownForce = 20f;
        [Tooltip("Physic layers checked to consider the player grounded")]
        public LayerMask GroundCheckLayers = -1;
        [Tooltip("distance from the bottom of the character controller capsule to test for grounded")]
        public float GroundCheckDistance = 0.05f;

        [Header("Movement")]
        [Tooltip("Max movement speed when grounded (when not sprinting)")]
        public float MaxSpeedOnGround = 2.5f;
        [Tooltip("Sharpness for the movement when grounded, a low value will make the player accelerate and decelerate slowly, a high value will do the opposite")]
        public float MovementSharpnessOnGround = 15;
        
        //no jump yet
        [Tooltip("Max movement speed when not grounded")]
        public float MaxSpeedInAir = 2.5f;
        [Tooltip("Acceleration speed when in the air")]
        public float AccelerationSpeedInAir = 25f;

        [Header("Rotation")]
        [Tooltip("Rotation speed for moving the camera")]
        public float RotationSpeed = 200f;
        [Range(0.1f, 1f)]
        
        [Tooltip("Rotation speed multiplier when aiming")]
        public float AimingRotationMultiplier = 0.4f;

        public Vector3 CharacterVelocity { get; set; }
        public bool IsGrounded { get; private set; }

        //set these as scriptable object and do modifiers to them
        public float CurrentSens { get; set; }

        public bool ShouldSprint { get; set; }
        public float RotationMultiplier
        {
            get
            {
                return CurrentSens;
            }
        }
        private float fovMultiplier;

        public float FOVmultiplier
        {
            get { return fovMultiplier; }
            set { fovMultiplier = value;
                CameraFOVset();
            }
        }

        //used to adapt FOV with the wonky camera angle the game uses
        private float currentCameraAngle;

        public float CurrentCameraAngle
        {
            get { return currentCameraAngle; }
            set { currentCameraAngle = value;
                CameraAngleSet();
            }
        }

        PlayerInputHandler m_InputHandler;
        CharacterController m_Controller;
        PlayerSettings m_Settings;
        public GameObject DebugText;
        
        float RigVerticalAngle = 0f;

        void Start()
        {
            // fetch components on the same gameObject
            //THIS IS THE STEP I ALWAYS FORGET TO DO
            m_Controller = GetComponent<CharacterController>();
            m_InputHandler = GetComponent<PlayerInputHandler>();
            m_Settings= GetComponent<PlayerSettings>();
            Menus.OnReset += ResetPositionOnReset;
        }

        private void ResetPositionOnReset()
        {
            //another quick fix?
            //cannot move while time is stopped
            Menus.Instance.CloseMenuElsewhere();
            transform.SetPositionAndRotation(new Vector3(0, 1, -5), Quaternion.Euler(0, 0, 0));
            Physics.SyncTransforms();
        }

        void Update()
        {
            HandleCharacterMovement();
            DebugText.GetComponent<DebugText>().SetText(PlayerStateThird.Instance.CurrentState.ToString());
        }

        private void HandleCharacterMovement()
        {
            // horizontal character rotation
            {
                // rotate the transform with the input speed around its local Y axis
                transform.Rotate(
                    new Vector3(0f, (m_InputHandler.GetLookInputsHorizontal() * RotationSpeed * RotationMultiplier),
                        0f), Space.Self);
            }
            // vertical camera rotation
            {
                //vert input to rig vertical angle
                RigVerticalAngle += m_InputHandler.GetLookInputsVertical() * RotationSpeed * RotationMultiplier;
                //Limit the vertical angle min/max
                RigVerticalAngle = Mathf.Clamp(RigVerticalAngle, -89f, 89f);
                //Apply vert angle as local rotation to transform "along it's right axis?"
                Rig.transform.localEulerAngles = new Vector3(RigVerticalAngle, 0, 0);
            }

            
            //check for sprinting
            bool SprintCheck()
            {
                if (Input.GetKey(KeyCode.LeftShift) && canSprint())
                    return true;
                else
                    return false;
            }
            bool canSprint()
            {
                if (movingForward() && (PlayerStateThird.Instance.CurrentState == PlayerStateThird.MoveState.Down)) 
                    return true;
                else
                    return false;
            }
            bool movingForward()
            {
                return (m_InputHandler.GetMoveInput().z > 0);
            }



            // character movement handling

            
            //holy ugh need to be able to multiply just one vector
            Vector3 SprintModifier = new Vector3(m_InputHandler.GetMoveInput().x, m_InputHandler.GetMoveInput().y, m_InputHandler.GetMoveInput().z * (SprintCheck() ? 2 : 1));

            // converts move input to a worldspace vector based on our character's transform orientation
            Vector3 worldspaceMoveInput = transform.TransformVector(SprintModifier);
            // calculate the desired velocity from inputs, max speed, and current slope
            Vector3 targetVelocity = worldspaceMoveInput * MaxSpeedOnGround;

            // smoothly interpolate between our current velocity and the target velocity based on acceleration speed
            CharacterVelocity = Vector3.Lerp(CharacterVelocity, targetVelocity,
            MovementSharpnessOnGround * Time.deltaTime);

            // Gets a reoriented direction that is tangent to a given slope
            m_Controller.Move(CharacterVelocity * Time.deltaTime);
        }
        public void CameraFOVset()
        {
            Camera.fieldOfView = Camera.HorizontalToVerticalFieldOfView(PlayerSettings.Instance.FOV + FOVmultiplier, Camera.aspect);
        }
        private void CameraAngleSet()
        {
            Camera.transform.localEulerAngles = new Vector3(+CurrentCameraAngle, 0f, 0f);
            
        }
    }
}
