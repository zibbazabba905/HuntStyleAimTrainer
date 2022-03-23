using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScoreScripts;

namespace TargetScripts
{
    public class TargetManager : MonoBehaviour
    {
        public static TargetManager Instance { get; private set; }

        //work this out obviously, object/script thing, eventually an array
        //public GameObject TD;
        //TargetDrill TargetDrill;
        public GameObject Target;
        public Transform RangeZero;
        
        //type of range, 360 or linear
        public bool FullCircle;
        
        //Max active targets at a time
        public int ActiveTargetMax;
        
        //Time between target spawns
        public float Delay;
        
        //Increase distance as game continues?
        public bool Distance = true;
        
        //Movement Type
        public enum Type { None, Left, Right, Wiggle, Random }
        public Type MovementType;

        //how wide the target zone is
        private float zoneLimitWidth = 5;

        //Distance for target spawning
        private int minDistance = 10;
        private int maxDistance;

        private int _activeTargetCount;
        public int ActiveTargetCount
        {
            get { return _activeTargetCount; }
            set { _activeTargetCount = value; }
        }


        void Start()
        {
            Instance = this;
            ActiveTargetCount = 0;
            maxDistance = minDistance;
        }

        void Update()
        {
            if (ActiveTargetCount < ActiveTargetMax)
            {
                ActiveTargetCount++;
                Invoke("SpawnTarget", Delay);
            }
        }
        public void SpawnTarget()
        {
            GameObject Clone;
            Quaternion verticalStraight = new Quaternion(0.7f, 0, 0, 0.7f);
            Clone = Instantiate(Target, targetLocation(), verticalStraight);
        }
        public void TargetHit()
        {
            ActiveTargetCount--;
        }
        private Vector3 targetLocation()
        {
            float targetHeight = 1.5f;
            float targetRandomX = Random.Range(-zoneLimitWidth,zoneLimitWidth);
            //every 10 hits add a new level of distance. Ignore score 0
            if (Distance)
            {
                if (ScoreManager.Instance.Hit % 10 == 0 && ScoreManager.Instance.Hit > 0)
                {
                    maxDistance += 10;
                }
            }
            float targetDistance = Random.Range(minDistance, maxDistance);


            Vector3 newPositon = new Vector3(targetRandomX,targetHeight,targetDistance);

            return RangeZero.position + newPositon;
        }
    }
}