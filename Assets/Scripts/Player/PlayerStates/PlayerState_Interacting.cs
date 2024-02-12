using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Interacting : BaseState_Player
{
    float timeDelay;
    Interactable interactable;
    public override void EnterState(PlayerController2 manager)
    {
       interactable = manager.interaction.closestInteractable;
        timeDelay = interactable.animationClip.length;
        manager.anim.SetBool(interactable.animParam, true);
    }

    public override void ExitState(PlayerController2 manager)
    {
        manager.EnterState(interactable.nextState);
    }

    public override void FrameUpdateState(PlayerController2 manager)
    {
        if (timeDelay > 0)
            timeDelay -= Time.deltaTime;
        if(timeDelay <= 0 && manager.anim.GetBool(interactable.animParam))
        {
            manager.interaction.InteractWith();
            manager.anim.SetBool(interactable.animParam, false);
            ExitState(manager);
        }
    }

    public override void PhysicsUpdateState(PlayerController2 manager)
    {

    }

    public override void OnTriggerEnter(PlayerController2 manager, Collider other)
    {
        
    }

    public override void OnTriggerExit(PlayerController2 manager, Collider other)
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
