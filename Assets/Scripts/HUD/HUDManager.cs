using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScoreScripts;

namespace ScoreScripts
{
    public class HUDManager : MonoBehaviour
    {
        public static HUDManager Instance { get; private set; }
        public Text MissText;
        public Text HitText;
        public Text ShotsText;

        void Start()
        {
            Instance = this;
        }
        void Update()
        {

        }
        public void TextUpdate(string category, int number)
        {
            if (category == "Miss")
                MissText.text = "Miss: " + number;
            if (category == "Hit")
                HitText.text = "Hit: " + number;
            if (category == "Shots")
                ShotsText.text = "Shots: " + number;
        }
    }
}