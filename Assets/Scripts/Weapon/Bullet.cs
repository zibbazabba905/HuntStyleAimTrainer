using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScoreScripts;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Invoke("TimeOut", 1);
    }
    void TimeOut()
    {
        TellScore(false);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        //TARGET LAYER IS CURRENTLY 6
        if (collision.gameObject.layer != 6)
        {
            TellScore(false);
        }
        Destroy(this.gameObject);
    }
    private void TellScore(bool call)
    {
        ScoreManager.Instance.MissCounter();
    }
}
