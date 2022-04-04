using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScoreScripts
{
    public class HitMarkerScript : MonoBehaviour
    {
        public static HitMarkerScript Instance { get; private set; }
        public GameObject hitmarker;
        void Start()
        {
            Instance = this;
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
