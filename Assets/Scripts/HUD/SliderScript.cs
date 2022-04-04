using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    Text Text;
    Slider Slider;
    void Start()
    {
        Text= GetComponentInChildren<Text>();
        Slider = GetComponent<Slider>();
        SetText();
    }

    public void SetText()
    {
        Text.text = gameObject.name + " " + (Mathf.Round(Slider.value * 100f) / 100f);
    }
}
