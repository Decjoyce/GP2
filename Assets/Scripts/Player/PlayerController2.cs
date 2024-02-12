using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour, PlayerControls.IPlayerOneActions, PlayerControls.IPlayerTwoActions
{
    PlayerControls playerControls;
    public PlayerInteraction interaction;
    public PlayerInventory inventory;
    public Animator anim;
    public Animator blockAnimTest;

    public bool player2;
    //Universal
    public CharacterController controller;
    [SerializeField] Camera playerCam;

    BaseState_Player currentState;
    public PlayerState_Neutral stateNeutral = new PlayerState_Neutral();
    public PlayerState_Sleep stateSleep = new PlayerState_Sleep();
    public PlayerState_Interacting stateInteract = new PlayerState_Interacting();

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

        currentState = stateNeutral;
        currentState.EnterState(this);
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
        currentState.FrameUpdateState(this);
    }

    void FixedUpdate()
    {
        currentState.PhysicsUpdateState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(this, other);
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        currentState.MovementState(this, ctx);
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        currentState.LookState(this, ctx);
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        currentState.InteractState(this, ctx);
    }

    public void OnUse(InputAction.CallbackContext ctx)
    {
        currentState.UseState(this, ctx);
    }

    public void EnterState(string n)
    {
        switch (n)
        {
            case "NEUTRAL":
                currentState = stateNeutral;
                currentState.EnterState(this);
                break;
            case "SLEEP":
                currentState = stateSleep;
                currentState.EnterState(this);
                break;
            case "INTERACT":
                currentState = stateInteract;
                currentState.EnterState(this);
                break;
            default:
                Debug.LogError("Player State: " + n + " does not exist");
                break;
        }
    }

    public void ExitState()
    {
        currentState.ExitState(this);
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