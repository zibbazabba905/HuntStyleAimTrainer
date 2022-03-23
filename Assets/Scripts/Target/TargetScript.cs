using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TargetScripts
{
    //these are all attempts to fix a physics problem
    public class TargetScript : MonoBehaviour
    {
        Rigidbody rb;

        void Start()
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            //rb.inertiaTensorRotation = new Quaternion(1, 1, 1, 1);
            //rb.inertiaTensor = new Vector3(1.2f, 0.2f, 0.2f);
            //rb.maxAngularVelocity = 1;

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Movement code will go here
        }
        private void OnCollisionEnter(Collision collision)
        {
            //Debug.Log(gameObject.GetComponent<Rigidbody>().maxAngularVelocity);
            //Debug.Log(gameObject.GetComponent<Rigidbody>().inertiaTensor);
            //Debug.Log(collision.contactCount);
            //Debug.Log(collision.GetContact(0).point);
            //Debug.Log(collision.impulse);
            //Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            //rb.useGravity = true;
            Destroy(gameObject);
        }
    }
}