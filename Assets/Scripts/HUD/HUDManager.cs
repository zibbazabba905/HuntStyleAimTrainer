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
        public void TextUpdate(string catagory, int number)
        {
            if (catagory == "Miss")
                MissText.text = "Miss: " + number;
            if (catagory == "Hit")
                HitText.text = "Hit: " + number;
            if (catagory == "Shots")
                ShotsText.text = "Shots: " + number;
        }
    }
}