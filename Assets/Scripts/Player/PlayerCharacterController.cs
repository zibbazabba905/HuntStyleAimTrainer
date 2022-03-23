using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;
using System;

namespace PlayerScripts
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInputHandler))]
    public class PlayerCharacterController : MonoBehaviour
    {
        //using camera "rig" instead of camera to hold objects seperate from camera view
        public GameObject Rig;

        //no jump yet
        public float GravityDownForce = 20f;
        [Tooltip("Physic layers checked to consider the player grounded")]
        public LayerMask GroundCheckLayers = -1;
        [Tooltip("distance from the bottom of the character controller capsule to test for grounded")]
        public float GroundCheckDistance = 0.05f;

        [Header("Movement")]
        [Tooltip("Max movement speed when grounded (when not sprinting)")]
        public float MaxSpeedOnGround = 10f;
        [Tooltip("Sharpness for the movement when grounded, a low value will make the player accelerate and decelerate slowly, a high value will do the opposite")]
        public float MovementSharpnessOnGround = 15;
        
        //no jump yet
        [Tooltip("Max movement speed when not grounded")]
        public float MaxSpeedInAir = 10f;
        [Tooltip("Acceleration speed when in the air")]
        public float AccelerationSpeedInAir = 25f;

        //no sprint yet
        [Tooltip("Multiplicator for the sprint speed (based on grounded speed)")]
        public float SprintSpeedModifier = 2f;

        [Header("Rotation")]
        [Tooltip("Rotation speed for moving the camera")]
        public float RotationSpeed = 200f;
        [Range(0.1f, 1f)]
        //no aim yet
        [Tooltip("Rotation speed multiplier when aiming")]
        public float AimingRotationMultiplier = 0.4f;

        public Vector3 CharacterVelocity { get; set; }
        public bool IsGrounded { get; private set; }
        public bool IsDead { get; private set; }

        public float RotationMultiplier
        {
            get
            {
                return 1f;
            }
        }
        PlayerInputHandler m_InputHandler;
        CharacterController m_Controller;
        float RigVerticalAngle = 0f;

        void Start()
        {
            // fetch components on the same gameObject
            //THIS IS THE STEP I ALWAYS FORGET TO DO
            m_Controller = GetComponent<CharacterController>();
            m_InputHandler = GetComponent<PlayerInputHandler>();
            IsDead = false; //TEST, DEAD DOES NOT MATTER YET CAN REMOVE MAYBE
        }

        void Update()
        {
            HandleCharacterMovement();
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

            // character movement handling

            // converts move input to a worldspace vector based on our character's transform orientation
            Vector3 worldspaceMoveInput = transform.TransformVector(m_InputHandler.GetMoveInput());
            // calculate the desired velocity from inputs, max speed, and current slope
            Vector3 targetVelocity = worldspaceMoveInput * MaxSpeedOnGround;

            // smoothly interpolate between our current velocity and the target velocity based on acceleration speed
            CharacterVelocity = Vector3.Lerp(CharacterVelocity, targetVelocity,
            MovementSharpnessOnGround * Time.deltaTime);

            // Gets a reoriented direction that is tangent to a given slope
            m_Controller.Move(CharacterVelocity * Time.deltaTime);
        }
    }
}