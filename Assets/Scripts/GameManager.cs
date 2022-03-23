using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameRunning;
    void Start()
    {
        Instance = this;
        IsGameRunning = true;
    }
    void Update()
    {
        
    }
}
