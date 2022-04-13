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
            while (HitMarkerScript.Instance.LastHit == this.gameObject)
                yield return null;
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
