using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerSettings : MonoBehaviour
    {
        public static PlayerSettings Instance { get; private set; }
        private bool gunslingerMode;
        public GameObject DebugText;
        public PlayerCharacterController controller;
        public bool GunslingerMode
        {
            get { return gunslingerMode; }
            set { gunslingerMode = value;
            }
        }
        //convert all these to scriptable objects
        //settings vs final numbers on things like FOV

        private float fov;
        public float FOV
        {
            get { return fov; }
            set { fov = value;
                controller.CameraFOVset();
                //try and convert this to int?
            }
        }


        private float downSens;
        public float DownSens
        {
            get { return downSens; }
            set { downSens = value;
            }
        }

        private float hipSens;
        public float HipSens
        {
            get { return hipSens; }
            set { hipSens = value;
            }
        }

        private float aimSens;
        public float AimSens
        {
            get { return aimSens; }
            set { aimSens = value;
            }
        }

        void Start()
        {
            Instance = this;

            //Preset settings? does not override unity settings
            controller = GetComponent<PlayerCharacterController>();
            GunslingerMode = false;
            DownSens = 1;
            HipSens = 1;
            AimSens = 1;
            FOV = 90;
        }
    }
}