using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScoreScripts
{
    public class LastHit : MonoBehaviour
    {
        //figure out colors for target, target may be too bright for hitmarkers to show up
        //but targets will eventually change
        void Start()
        {
            HitMarkerScript.Instance.LastHit = this.gameObject;
            StartCoroutine(LastHitCheck());
        }
        private IEnumerator LastHitCheck()
        {
            //make last hit stand out in color and above others then on next hit resess into target and different color
            this.gameObject.transform.localPosition += new Vector3(0, 0, -0.1f);
            while (HitMarkerScript.Instance.LastHit == this.gameObject)
                yield return null;
            this.gameObject.transform.localPosition -= new Vector3(0, 0, -0.1f);
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
