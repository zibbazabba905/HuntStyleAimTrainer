using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    public static Menus Instance { get; private set; }
    
    [Header("Menus")]
    public GameObject MenuMain;
    public GameObject MenuPlayer;
    public GameObject MenuGame;
    public GameObject MenuInfo;
    public GameObject HUD;
    public GameObject Crosshairs;

    //I should be able to link this all to button.OnClick, or I should put this OnReset somewhere else
    public static event Action OnReset;

    private bool inMenus;
    private bool escapeInputHappened;

    void Start()
    {
        Instance = this;
        StartInMenu();
        PlayerScripts.PlayerStateThird.StateChange += OnStateChange;
    }

    private void OnStateChange(StateData NewState)
    {
        Crosshairs.SetActive(NewState.CrosshairActive);
    }

    private void StartInMenu()
    {
        //quick fix to start the game in the menu
        inMenus = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        HUD.SetActive(false);
        MenuMain.SetActive(true);

    }
    void Update()
    {
        if (GetEscapeInput())
        {
            //enter menus
            if (!inMenus)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Time.timeScale = 0;
                HUD.SetActive(false);
                MenuMain.SetActive(true);
            }
            //exit menus
            else
            {
                closeMenus();
            }
            inMenus = !inMenus;
        }
    }
    private void closeMenus()
    {
        foreach (Transform child in transform)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            HUD.SetActive(true);
            child.gameObject.SetActive(false);
        }
    }
    public void CloseMenuElsewhere()
    {
        closeMenus();
        inMenus = false;
    }

    //Key Inputs
    private void LateUpdate()
    {
        escapeInputHappened = Application.isEditor ? Input.GetButton("Tab") : Input.GetButton("Cancel");
    }
    public bool GetEscapeInput()
    {
        return !escapeInputHappened && (Application.isEditor ? Input.GetButton("Tab") : Input.GetButton("Cancel"));
    }

    //Main Menu Button Functions
    public void OpenPlayerMenu()
    {
        MenuMain.SetActive(false);
        MenuPlayer.SetActive(true);
    }
    public void OpenGameMenu()
    {
        MenuMain.SetActive(false);
        MenuGame.SetActive(true);
    }
    public void OpenInfoMenu()
    {
        MenuMain.SetActive(false);
        MenuInfo.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Clicked");
        Application.Quit();
    }

    public void ResetGame()
    {
        OnReset?.Invoke();
    }
}
