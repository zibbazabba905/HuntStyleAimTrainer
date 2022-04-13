using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    //used for state text display currently
    public void SetText(string testSubject)
    {
        gameObject.GetComponent<Text>().text = testSubject;
    }
}
