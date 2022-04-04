using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerSettings : MonoBehaviour
    {
        public static PlayerSettings Instance { get; private set; }
        private bool gunslingerMode;
        public bool GunslingerMode
        {
            get { return gunslingerMode; }
            set { gunslingerMode = value;
            }
        }

        private float fov;
        public float FOV
        {
            get { return fov; }
            set { fov = value;
                //NULL CHECK AGAIN?
                //now done in the SliderScript
                //Menus.Instance.UpdateSliderText(Menus.Instance.FOVSlider, value);
            }
        }


        private float downSens;
        public float DownSens
        {
            get { return downSens; }
            set { downSens = value;
                //Menus.Instance.UpdateSliderText(Menus.Instance.DownSlider, value);
            }
        }

        private float hipSens;
        public float HipSens
        {
            get { return hipSens; }
            set { hipSens = value;
                //Menus.Instance.UpdateSliderText(Menus.Instance.HipSlider, value);
            }
        }

        private float aimSens;
        public float AimSens
        {
            get { return aimSens; }
            set { aimSens = value;
                //Menus.Instance.UpdateSliderText(Menus.Instance.AimSlider, value);
            }
        }

        void Start()
        {
            Instance = this;

            //Preset settings?
            GunslingerMode = false;
            DownSens = 1;
            HipSens = 1;
            AimSens = 1;
            FOV = 90;
        }
    }
}