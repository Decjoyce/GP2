using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerControls.IPlayerOneActions, PlayerControls.IPlayerTwoActions
{
    PlayerControls playerControls;
    PlayerInteraction interaction;
    PlayerInventory inventory;

    public bool player2;
    //Universal
    CharacterController controller;
    [SerializeField] Camera playerCam;
    //Stats
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    float turnSmoothVelocity;
    Vector2 movementDirection;
    float lookDirection;

    void Awake()
    {
        playerControls = new PlayerControls();

        if (!player2)
            playerControls.PlayerOne.SetCallbacks(this);
        else
            playerControls.PlayerTwo.SetCallbacks(this);

        interaction = GetComponentInChildren<PlayerInteraction>();
        inventory = GetComponent<PlayerInventory>();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();   
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, lookDirection * turnSpeed * Time.deltaTime, 0);

        if (movementDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.y) * Mathf.Rad2Deg;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * transform.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        movementDirection = ctx.ReadValue<Vector2>().normalized;
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        lookDirection = ctx.ReadValue<float>();
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
            interaction.CheckInteraction();
    }

    public void OnUse(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            inventory.CheckUse(true);
        else if(ctx.canceled)
            inventory.CheckUse(false);

    }

    #region stuff
    /*    Shush shush zone

            if (LookDirection.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(LookDirection.x, LookDirection.y) * Mathf.Rad2Deg + playerCam.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSpeed);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }*/
    #endregion
}
