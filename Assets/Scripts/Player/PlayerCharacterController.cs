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
        public Camera m_camera;


        [Header("Movement")]
        [Tooltip("Max movement speed when grounded (when not sprinting)")]
        public float MaxSpeedOnGround = 2.5f;
        [Tooltip("Sharpness for the movement when grounded, a low value will make the player accelerate and decelerate slowly, a high value will do the opposite")]
        public float MovementSharpnessOnGround = 15;

        //math out this rotation speed stuff eventually
        //need to figure out game and this thing's cm / 360 rotation
        [Header("Rotation")]
        [Tooltip("Rotation speed for moving the camera")]
        public float RotationSpeed = 200f;

/*
        //no jump yet
        [Header("No Jump Yet")]
        public float GravityDownForce = 20f;
        [Tooltip("Physic layers checked to consider the player grounded")]
        public LayerMask GroundCheckLayers = -1;
        [Tooltip("distance from the bottom of the character controller capsule to test for grounded")]
        public float GroundCheckDistance = 0.05f;
        [Tooltip("Max movement speed when not grounded")]
        public float MaxSpeedInAir = 2.5f;
        [Tooltip("Acceleration speed when in the air")]
        public float AccelerationSpeedInAir = 25f;
        //public bool IsGrounded { get; private set; }

 */

        [SerializeField] private StateData stateData;



        public Vector3 CharacterVelocity { get; set; }

        private bool gunslingerMode;
        public bool GunslingerMode
        {
            get { return gunslingerMode; }
            set
            {
                gunslingerMode = value;
            }
        }

        private float baseFOV;
        public float BaseFOV
        {
            get { return baseFOV; }
            set
            {
                baseFOV = value;
                //set camera angle off of fov
            }
        }

        PlayerInputHandler m_InputHandler;
        CharacterController m_Controller;
        public GameObject DebugText;

        //GF is base camera angle
        private float GF = 0;
        
        float RigVerticalAngle = 0f;
        
        void Start()
        {
            // fetch components on the same gameObject
            //THIS IS THE STEP I ALWAYS FORGET TO DO
            m_Controller = GetComponent<CharacterController>();
            m_InputHandler = GetComponent<PlayerInputHandler>();

            baseFOV = 90f;

            Menus.OnReset += ResetPositionOnReset;
            PlayerStateThird.StateChange += onStateChange;
        }

        //actions
        private void onStateChange(StateData newStateData)
        {
            stateData = newStateData;
            StopCoroutine("OffsetCamera");
            StartCoroutine("OffsetCamera", Camera.VerticalToHorizontalFieldOfView(m_camera.fieldOfView, m_camera.aspect));
            //StopCoroutine("lerpCameraAngle");
            //StartCoroutine("lerpCameraAngle", GF);
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

            //DebugText.GetComponent<DebugText>().SetText(PlayerStateThird.Instance.CurrentState.ToString());
        }

        private void HandleCharacterMovement()
        {
            // horizontal character rotation
            {
                // rotate the transform with the input speed around its local Y axis
                transform.Rotate(
                    new Vector3(0f, (m_InputHandler.GetLookInputsHorizontal() * RotationSpeed * stateData.Sens),
                        0f), Space.Self);
            }
            // vertical camera rotation
            {
                //vert input to rig vertical angle
                RigVerticalAngle += m_InputHandler.GetLookInputsVertical() * RotationSpeed * stateData.Sens;
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

        //camera settings
        IEnumerator OffsetCamera(float currentFOV)
        {
            float timeElapsed = 0;
            float lerpDuration = 0.25f;
            float angleTest = m_camera.fieldOfView * 0.166667f;
            while (timeElapsed < lerpDuration)
            {
                float lerpedFOV = Mathf.Lerp(currentFOV, baseFOV + stateData.FOVChange, timeElapsed / lerpDuration);
                m_camera.fieldOfView = Camera.HorizontalToVerticalFieldOfView(lerpedFOV, m_camera.aspect);
                angleTest = m_camera.fieldOfView * 0.1f;
                DebugText.GetComponent<DebugText>().SetText("blah" + ( m_camera.fieldOfView / angleTest));
                m_camera.transform.localEulerAngles = new Vector3(-angleTest, 0f, 0f);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}