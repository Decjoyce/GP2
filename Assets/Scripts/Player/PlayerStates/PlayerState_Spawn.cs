using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Spawn : BaseState_Player
{

    public override void EnterState(PlayerController2 manager)
    {
        manager.anim.SetBool("wakeUp", true);
    }

    public override void ExitState(PlayerController2 manager)
    {
        manager.anim.SetBool("wakeUp", false);
        manager.EnterState("NEUTRAL");
    }

    public override void FrameUpdateState(PlayerController2 manager)
    {

    }

    public override void PhysicsUpdateState(PlayerController2 manager)
    {

    }

    public override void AnimationState(PlayerController2 manager)
    {

    }

    public override void InteractState(PlayerController2 manager, InputAction.CallbackContext ctx)
    {

    }

    public override void UseState(PlayerController2 manager, InputAction.CallbackContext ctx)
    {
    }

    public override void MovementState(PlayerController2 manager, InputAction.CallbackContext ctx)
    {

    }

    public override void LookState(PlayerController2 manager, InputAction.CallbackContext ctx)
    {

    }

}
