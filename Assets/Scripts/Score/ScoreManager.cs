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

        private int _miss;
        public int Miss
        {
            get { return _miss; }
            set { _miss = value;
                MissText.text = "Miss: " + _miss;
            }
        }

        private int _hit;
        public int Hit
        {
            get { return _hit; }
            set { _hit = value;
                HitText.text = "Hit: " + _hit;
            }
        }


        private int _shots;
        public int Shots
        {
            get { return _shots; }
            set { _shots = value;
                ShotsText.text = "Shots: " + _shots;
            }
        }



        void Start()
        {
            Instance = this;
            Hit = 0;
            Miss = 0;
            Shots = 0;
        }
        public void MissCounter()
        {
            Miss++;
        }
        public void ShotCounter()
        {
            Shots++;
        }
        public void ComputeScore(Vector3 LocalPoints)
        {
            Hit++;
        }
        private void UpdateHudManager(string label, int number)
        {
            //HudManager.Instance.UpdateHudText(label, number);
        }
    }
}