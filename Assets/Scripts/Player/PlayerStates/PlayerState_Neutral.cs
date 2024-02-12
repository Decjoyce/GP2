using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerState_Neutral : BaseState_Player
{

    //Stats
    [Header("Values")]
    [SerializeField] float speed = 3;
    [SerializeField] float turnSpeed = 180;
    float turnSmoothVelocity;
    Vector2 movementDirection;
    float lookDirection;

    public override void EnterState(PlayerController2 manager)
    {

    }

    public override void FrameUpdateState(PlayerController2 manager)
    {
        manager.transform.Rotate(0, lookDirection * turnSpeed * Time.deltaTime, 0);

        if (movementDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.y) * Mathf.Rad2Deg;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * manager.transform.forward;
            manager.controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    public override void PhysicsUpdateState(PlayerController2 manager)
    {
        float lerpedX = Mathf.Lerp(manager.anim.GetFloat("xinput"), movementDirection.x, 0.2f);
        float lerpedZ = Mathf.Lerp(manager.anim.GetFloat("zinput"), movementDirection.y, 0.2f);
        float lerpedTurn = Mathf.Lerp(manager.anim.GetFloat("turninput"), lookDirection, 0.2f);
        manager.anim.SetFloat("xinput", lerpedX);
        manager.anim.SetFloat("zinput", lerpedZ);
        manager.anim.SetFloat("turninput", lerpedTurn);
        manager.anim.SetFloat("movementInput", movementDirection.magnitude);
    }

    public override void OnTriggerEnter(PlayerController2 manager, Collider other)
    {
        if (other.CompareTag("Trigg/Right"))
            manager.blockAnimTest.SetTrigger("pushRight");
        if (other.CompareTag("Trigg/Left"))
            manager.blockAnimTest.SetTrigger("pushLeft");
        if (other.CompareTag("Trigg/Up"))
            manager.blockAnimTest.SetTrigger("pushUp");
        if (other.CompareTag("Trigg/Down"))
            manager.blockAnimTest.SetTrigger("pushDown");
    }

    public override void AnimationState(PlayerController2 manager)
    {

    }

    public override void InteractState(PlayerController2 manager, InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            manager.interaction.CheckInteraction();
    }

    public override void UseState(PlayerController2 manager, InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            manager.inventory.CheckUse(true);
        else if (ctx.canceled)
            manager.inventory.CheckUse(false);
    }

    public override void MovementState(PlayerController2 manager, InputAction.CallbackContext ctx)
    {
        movementDirection = ctx.ReadValue<Vector2>().normalized;
    }

    public override void LookState(PlayerController2 manager, InputAction.CallbackContext ctx)
    {
        lookDirection = ctx.ReadValue<float>();
    }
}
