using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;
using Patterns;

public class PlayerMovementState : MonoBehaviour
{
    public static PlayerMovementState Instance { get; private set; }
    public bool ShouldSprint;
    public enum MovementEnum { Down, Hip, Aim }
    FSM movementState = new FSM();

    void Start()
    {
        Instance = this;
        //bet this can be looped
        movementState.Add((int)MovementEnum.Down, new Down(movementState));
        movementState.Add((int)MovementEnum.Hip, new Hip(movementState));
        movementState.Add((int)MovementEnum.Aim, new Aim(movementState));

        movementState.SetCurrentState(movementState.GetState((int)MovementEnum.Down));
        ShouldSprint = Input.GetKey(KeyCode.LeftShift);
    }

    void Update()
    {
        
        if (movementState != null)
        {
            movementState.Update();
        }
    }
}
public class Down : State
{
    
    public Down(FSM fsm) : base(fsm)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("down Enter");
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Down Exit");
    }
    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButton(1))
        {
            m_fsm.SetCurrentState(m_fsm.GetState((int)PlayerMovementState.MovementEnum.Hip));
        }

        //work this in somehow?
        //PlayerMovementState.Instance.ShouldSprint = Input.GetKey(KeyCode.LeftShift);
    }

}
public class Hip : State
{
    public Hip(FSM fsm) : base(fsm)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Hip Enter");

    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Hip Exit");

    }
    public override void Update()
    {

        base.Update();
        //important to use GetKeyDown instead of GetKey
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_fsm.SetCurrentState(m_fsm.GetState((int)PlayerMovementState.MovementEnum.Aim));
        }
        else if (!Input.GetMouseButton(1))
        {
            m_fsm.SetCurrentState(m_fsm.GetState((int)PlayerMovementState.MovementEnum.Down));
        }
    }

}
public class Aim : State
{
    public Aim(FSM fsm) : base(fsm)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Aim Enter");

    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Aim Exit");

    }
    public override void Update()
    {
        base.Update();
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            m_fsm.SetCurrentState(m_fsm.GetState((int)PlayerMovementState.MovementEnum.Hip));
        }
        else if (!Input.GetMouseButton(1))
        {
            m_fsm.SetCurrentState(m_fsm.GetState((int)PlayerMovementState.MovementEnum.Down));
        }

    }

}