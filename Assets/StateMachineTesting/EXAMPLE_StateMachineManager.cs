using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXAMPLE_StateMachineManager : MonoBehaviour
{
    EXAMPLE_BaseState currentState;
    public EXAMPLE_State1 state1 = new EXAMPLE_State1();
    public EXAMPLE_State2 state2 = new EXAMPLE_State2();
    public EXAMPLE_State3 state3 = new EXAMPLE_State3();

    // Start is called before the first frame update
    void Start()
    {
        currentState = state1;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.FrameUpdateState(this);
    }

    public void SwitchState(EXAMPLE_BaseState state)
    {
        currentState = state;

        currentState.EnterState(this);
    }
}
