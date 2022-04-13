using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScoreScripts
{
    public class HitMarkerScript : MonoBehaviour
        //this could have a lot of the same things as the target manager
        //make more generic spawner
        //see target manager script
    {
        public static HitMarkerScript Instance { get; private set; }
        public GameObject hitmarker;
        public GameObject LastHit;
        void Start()
        {
            Instance = this;
            Menus.OnReset += DeleteMarkersOnReset;
        }

        private void DeleteMarkersOnReset()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void ComputePoint(Vector3 targetLocal)
        {
            //useing x and z because "target" is rotated but display target is not
            Vector3 newPoint = new Vector3(targetLocal.x, targetLocal.z, 0f);
            spawnHitMarker(newPoint);
        }
        private void spawnHitMarker(Vector3 hitPoint)
        {
            GameObject hmClone = Instantiate(hitmarker, transform);
            hmClone.transform.localPosition += hitPoint; ;
        }
    }
}
