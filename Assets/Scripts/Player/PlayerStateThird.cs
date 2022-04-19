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

        //yes I'm still thinking about this, 3 states, 2 inputs and a bool. Currently works.
        //the game I'm basing this on was probably done in flowgraph so anything is better.

        public static PlayerStateThird Instance { get; private set; }
        public enum MoveState { Down, Hip, Aim }
        public MoveState CurrentState = MoveState.Down;
        //init state problem
        private MoveState lateState = MoveState.Hip;
        public PlayerCharacterController Controller;

        public static event Action<StateData> StateChange;

        [Header("State Variables")]
        public StateData DownData;
        public StateData HipData;
        public StateData AimData;

        void Start()
        {
            Instance = this;
            Controller = GetComponent<PlayerCharacterController>();
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
            StateChange.Invoke(DownData);
            StartCoroutine(ExitDown());
        }
        private void Hip()
        {
            StateChange.Invoke(HipData);
            StartCoroutine(ExitHip());
        }
        private void Aim()
        {
            StateChange.Invoke(AimData);
            StartCoroutine(ExitAim());
        }
        private IEnumerator ExitDown()
        {
            while (CurrentState == MoveState.Down)
            {
                if (Controller.GunslingerMode ? !Input.GetKey(KeyCode.LeftShift) : Input.GetMouseButton(1))
                    CurrentState = MoveState.Hip;
                else if (Controller.GunslingerMode && Input.GetMouseButton(1))
                    CurrentState = MoveState.Aim;
                yield return null;
            }
        }
        private IEnumerator ExitHip()
        {
            while (CurrentState == MoveState.Hip)
            {
                if (Controller.GunslingerMode ? (Input.GetKey(KeyCode.LeftShift) && IsMoving()) : !Input.GetMouseButton(1))
                    CurrentState = MoveState.Down;
                //GetKeyDown here
                else if (Controller.GunslingerMode ? Input.GetMouseButton(1) : Input.GetKeyDown(KeyCode.LeftShift))
                    CurrentState = MoveState.Aim;
                yield return null;
            }
        }
        private IEnumerator ExitAim()
        {
            while (CurrentState == MoveState.Aim)
            {
                //GetKeyDown here
                if (Controller.GunslingerMode ? (Input.GetKey(KeyCode.LeftShift) && IsMoving()) : !Input.GetMouseButton(1))
                    CurrentState = MoveState.Down;
                else if (Controller.GunslingerMode? !Input.GetMouseButton(1) : !Input.GetKey(KeyCode.LeftShift))
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