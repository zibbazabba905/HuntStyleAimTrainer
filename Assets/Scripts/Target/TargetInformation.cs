using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScoreScripts;


namespace TargetScripts
{
    public class TargetInformation : MonoBehaviour
    {
        bool TargetBeenHit;
        void Start()
        {
            TargetBeenHit = false;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (!TargetBeenHit)
            {
                ScoreManager.Instance.ComputeScore(transform.InverseTransformPoint(collision.GetContact(0).point));
                TargetManager.Instance.TargetHit();
                TargetBeenHit = true;
            }
        }
    }
}