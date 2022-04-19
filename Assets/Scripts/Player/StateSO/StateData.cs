using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Variables")]
public class StateData : ScriptableObject
{
    public float Sens;
    public float FOVChange;
    public float CameraAngle;
    public Vector3 WeaponPosition;
    public Vector3 WeaponRotation;
    public bool CrosshairActive;
    public void SetSens(float value)
    {
        Sens = value;
    }
}

