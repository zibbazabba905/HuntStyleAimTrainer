using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScoreScripts
{
    //make this more generic?
    public class HitDetector : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            //change from global point to local point on target
            HitMarkerScript.Instance.ComputePoint(transform.InverseTransformPoint(collision.GetContact(0).point));
        }
    }
}