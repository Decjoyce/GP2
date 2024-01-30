using UnityEngine;

public class EXAMPLE_State1 : EXAMPLE_BaseState
{
    public override void EnterState(EXAMPLE_StateMachineManager manager)
    {
        Debug.Log("Entered state 1");
    }

    public override void FrameUpdateState(EXAMPLE_StateMachineManager manager)
    {
        Debug.Log("o");
    }
}
