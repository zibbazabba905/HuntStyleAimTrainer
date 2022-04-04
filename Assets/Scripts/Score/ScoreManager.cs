using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TargetScripts;
using UnityEngine.UI;

namespace ScoreScripts
{
    //MAKE A HUD MANAGER AS WELL
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }
        public Text MissText;
        public Text HitText;
        public Text ShotsText;

        private int _miss = 0;
        public int Miss
        {
            get { return _miss; }
            set { _miss = value;
                UpdateHud("Miss", _miss);
            }
        }

        private int _hit = 0;
        public int Hit
        {
            get { return _hit; }
            set { _hit = value;
                UpdateHud("Hit", _hit);
            }
        }

        private int _shots = 0;
        public int Shots
        {
            get { return _shots; }
            set { _shots = value;
                UpdateHud("Shots", _shots);
            }
        }
        private void UpdateHud(string category, int number)
        {
            //because ScoreManager probably starts before HUDManager
            //if (HUDManager.Instance !=null)
                HUDManager.Instance?.TextUpdate(category, number);
        }
        void Start()
        {
            Instance = this;
        }
        //rework this 
        public void MissCounter()
        {
            Miss++;
        }
        public void ShotCounter()
        {
            Shots++;
        }
        public void HitCounter()
        {
            Hit++;
        }
    }
}