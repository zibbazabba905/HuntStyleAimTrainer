using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TargetScripts
{
    //make this more generic
    public class TargetScript : MonoBehaviour
    {
        private bool wasHit = false;
        void OnCollisionEnter(Collision collision)
        {
            if (!wasHit)
            {
                //make real and add force because physics engine doesn't like continuous collision
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                if (collision.rigidbody)
                    rb.AddForceAtPosition((collision.collider.transform.forward * 25), collision.GetContact(0).point, ForceMode.Impulse);

                //subtract target from active count and add hit to score
                TargetManager.Instance.TargetHit();
                ScoreScripts.ScoreManager.Instance.HitCounter();
                Destroy(gameObject, 2);
                wasHit = true;
            } 
        }
    }
}