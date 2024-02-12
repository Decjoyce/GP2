using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Sleep : BaseState_Player
{

    public override void EnterState(PlayerController2 manager)
    {
        GameManager.instance.sleepManager.PlayerSleep(manager.player2);
        manager.anim.SetBool("isSleeping", true);
    }

    public override void ExitState(PlayerController2 manager)
    {
        manager.anim.SetBool("isSleeping", false);
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
        if (ctx.performed)
        {
            GameManager.instance.sleepManager.PlayerAwake(manager.player2);
            manager.ExitState();
        }
        else if (ctx.canceled)
        {
            GameManager.instance.sleepManager.PlayerAwake(manager.player2);
            manager.ExitState();
        }
    }

    public override void MovementState(PlayerController2 manager, InputAction.CallbackContext ctx)
    {

    }

    public override void LookState(PlayerController2 manager, InputAction.CallbackContext ctx)
    {

    }

}
