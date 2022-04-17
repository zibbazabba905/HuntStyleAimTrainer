using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerStateThird : MonoBehaviour
    {
        //if current state != late state
            //do current state start
                //set properties
                //start exit coroutine
                    //on exit set current state
                //late state == current state

        //yes I'm still thinking about this, 3 states, 2 inputs and a bool. Currently works. Adding more coroutines/events later.
        //the game I'm basing this on was probably done in flowgraph so anything is better.

        public static PlayerStateThird Instance { get; private set; }
        public enum MoveState { Down, Hip, Aim }
        public MoveState CurrentState = MoveState.Down;
        //init state problem
        private MoveState lateState = MoveState.Hip;
        public PlayerCharacterController Controller;
        public PlayerSettings Settings;
        public GameObject Crosshairs;

        public static event Action<StateData> StateChange;

        [Header("State Variables")]
        public StateData DownData;
        public StateData HipData;
        public StateData AimData;

        void Start()
        {
            Instance = this;
            Controller = GetComponent<PlayerCharacterController>();
            Settings = GetComponent<PlayerSettings>();
        }

        void Update()
        {
            if (CurrentState != lateState)
            {
                //make part of coroutine as well?
                Invoke(CurrentState.ToString(), 0);
                lateState = CurrentState;
            }
        }
        //lerp all these settings
        private void Down()
        {
            //set properties
            StateChange.Invoke(DownData);
            /*Controller.CurrentSens = Settings.DownSens;
            Controller.FOVmultiplier = 15;
            Controller.CurrentCameraAngle = -8.5f;
            Crosshairs.SetActive(false);
            */
            StartCoroutine(ExitDown());
        }
        private void Hip()
        {
            //set properties
            StateChange.Invoke(HipData);
            /*Controller.CurrentSens = Settings.HipSens;
            Controller.FOVmultiplier = 0;
            Controller.CurrentCameraAngle = -6.5f;
            Crosshairs.SetActive(true);
            */
            StartCoroutine(ExitHip());
        }
        private void Aim()
        {
            //set properties
            StateChange.Invoke(AimData);
            /*Controller.CurrentSens = Settings.AimSens;
            Controller.FOVmultiplier = -20;
            Controller.CurrentCameraAngle = -4.5f;
            Crosshairs.SetActive(true);
            */
            StartCoroutine(ExitAim());
        }
        private IEnumerator ExitDown()
        {
            while (CurrentState == MoveState.Down)
            {
                if (PlayerSettings.Instance.GunslingerMode ? !Input.GetKey(KeyCode.LeftShift) : Input.GetMouseButton(1))
                    CurrentState = MoveState.Hip;
                else if (PlayerSettings.Instance.GunslingerMode && Input.GetMouseButton(1))
                    CurrentState = MoveState.Aim;
                yield return null;
            }
        }
        private IEnumerator ExitHip()
        {
            while (CurrentState == MoveState.Hip)
            {
                if (PlayerSettings.Instance.GunslingerMode ? (Input.GetKey(KeyCode.LeftShift) && IsMoving()) : !Input.GetMouseButton(1))
                    CurrentState = MoveState.Down;
                //GetKeyDown here
                else if (PlayerSettings.Instance.GunslingerMode ? Input.GetMouseButton(1) : Input.GetKeyDown(KeyCode.LeftShift))
                    CurrentState = MoveState.Aim;
                yield return null;
            }
        }
        private IEnumerator ExitAim()
        {
            while (CurrentState == MoveState.Aim)
            {
                //GetKeyDown here
                if (PlayerSettings.Instance.GunslingerMode ? (Input.GetKeyDown(KeyCode.LeftShift) && IsMoving()) : !Input.GetMouseButton(1))
                    CurrentState = MoveState.Down;
                else if (PlayerSettings.Instance.GunslingerMode? !Input.GetMouseButton(1) : !Input.GetKey(KeyCode.LeftShift))
                    CurrentState = MoveState.Hip;
                yield return null;
            }
        }
        private bool IsMoving()
        {
            //fit this somwhere else? Character Controller
            return (PlayerInputHandler.Instance.GetMoveInput().z > 0);
        }
    }
}