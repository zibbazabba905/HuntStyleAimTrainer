using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScoreScripts;

namespace TargetScripts
{
    public class TargetManager : MonoBehaviour
    {
        //make more generic
        //should be seperated into check/logic/spawn scripts to become more generic
        //should also be able to turn into a bulletspawner in a bullet-hell style game
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
        public enum Type { None, Left, Right, Wiggle, Random, Test }
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
            Menus.OnReset += ResetDistanceOnReset;
        }

        private void ResetDistanceOnReset()
        {
            maxDistance = minDistance;
        }

        void Update()
        {
            //make a coroutine instead?
            if (ActiveTargetCount < ActiveTargetMax)
            {
                ActiveTargetCount++;
                Invoke("SpawnTarget", Delay);
            }
        }
        public void SpawnTarget()
        {
            GameObject Clone;
            //use Qtest to get Quaternion numbers
            //or switch to eulerangle version
            Quaternion verticalStraight = new Quaternion(-0.7f, 0, 0, 0.7f);
            Clone = Instantiate(Target, targetLocation(), verticalStraight);
        }
        public void TargetHit()
        {
            ActiveTargetCount--;
        }
        private Vector3 targetLocation()
        {
            float targetHeight = 1.5f;
            float targetRandomX = UnityEngine.Random.Range(-zoneLimitWidth,zoneLimitWidth);
            //every 10 hits add a new level of distance. Ignore score 0
            if (Distance)
            {
                if (ScoreManager.Instance.Hit % 10 == 0 && ScoreManager.Instance.Hit > 0)
                {
                    maxDistance += 10;
                }
            }
            float targetDistance = UnityEngine.Random.Range(minDistance, maxDistance);
            if (MovementType == Type.Test)
            {
                targetRandomX = 0f;
                targetDistance = 10f;
            }
            Vector3 newPositon = new Vector3(targetRandomX,targetHeight,targetDistance);

            return RangeZero.position + newPositon;
        }
    }
}