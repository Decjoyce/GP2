using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseState_Player
{
    public virtual void EnterState(PlayerController2 manager)
    {

    }

    public virtual void ExitState(PlayerController2 manager)
    {

    }

    public virtual void FrameUpdateState(PlayerController2 manager)
    {

    }

    public virtual void PhysicsUpdateState(PlayerController2 manager)
    {

    }

    public virtual void OnTriggerEnter(PlayerController2 manager, Collider other)
    {

    }

    public virtual void OnTriggerExit(PlayerController2 manager, Collider other)
    {

    }

    public virtual void AnimationState(PlayerController2 manager)
    {

    }

    public virtual void InteractState(PlayerController2 manager, InputAction.CallbackContext ctx)
    {

    }

    public virtual void UseState(PlayerController2 manager, InputAction.CallbackContext ctx)
    {

    }

    public virtual void MovementState(PlayerController2 manager, InputAction.CallbackContext ctx)
    {

    }

    public virtual void LookState(PlayerController2 manager, InputAction.CallbackContext ctx)
    {

    }

}
