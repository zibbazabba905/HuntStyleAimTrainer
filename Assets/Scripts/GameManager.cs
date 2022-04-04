using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameRunning;
    public static bool GameIsPaused;

    public enum State { MAIN, PLAYER, GAME, RUN, TEST }
    public State GameState;

    void Start()
    {
        Instance = this;
        IsGameRunning = true;


    }
    void Update()
    {
        
    }
    public static void PauseGame()
    {

    }
}
