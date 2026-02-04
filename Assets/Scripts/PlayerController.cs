using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public enum PlayerState { Idle, MoveBackwards, Run, Walk, Jumping, Firing, Saluting }
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameObject model;

    [Header("Movimiento")]
    public float velocidadAndando = 3f;
    public float velocidadCorriendo = 5f;
    public float velocidadHaciaAtras = 2f;
    public float velocidadGiro = 120f;
    private float velocidadVertical = 0f;
    public float gravedad = 9.8f;

    private CharacterController characterController;
    private Animator animator;
    private PlayerState currentState = PlayerState.Idle;

    private PlayerInput playerInput;

    // Actions
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction fireAction;
    private InputAction saluteAction;
    private InputAction runAction;

    // Input Storage
    private Vector2 moveValue;
    private bool jumpPressed;
    private bool firePressed;
    private bool salutePressed;
    private bool runPressed;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = model.GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("Player Input");
            return;
        }

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        fireAction = playerInput.actions["Fire"];
        saluteAction = playerInput.actions["Salute"];
        runAction = playerInput.actions["Run"];
    }

    private void Update()
    {
        updateSensor();
        updateState();
        updateAction();
    }

    /**
     * React to player input
    */
    private void updateSensor()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        jumpPressed = jumpAction.WasPressedThisFrame();
        firePressed = fireAction.WasPressedThisFrame();
        salutePressed = saluteAction.WasPressedThisFrame();
        runPressed = runAction.IsPressed();
    }

    private void updateState()
    {
        if (moveValue.y == 0)
        {
            currentState = PlayerState.Idle;
        }
        else if (moveValue.y < 0)
        {
            currentState = PlayerState.MoveBackwards;
        }
        else if (runPressed)
        {
            currentState = PlayerState.Run;
        }
        else
        {
            currentState = PlayerState.Walk;
        }

        if (firePressed) currentState = PlayerState.Firing;
        if (salutePressed) currentState = PlayerState.Saluting;
        if (jumpPressed && characterController.isGrounded) currentState = PlayerState.Jumping;
    }

    private void updateAction()
    {
        // Rotate
        float anguloGiro = moveValue.x * velocidadGiro * Time.deltaTime;
        transform.Rotate(0f, anguloGiro, 0f);

        // Gravity
        if (characterController.isGrounded){
            velocidadVertical = -0.5f;
        }
        else
        {
            velocidadVertical -= gravedad * Time.deltaTime;
        }
        // Move
        Vector3 movimientoLocal = Vector3.zero;
        switch (currentState)
        {
            case PlayerState.Idle:
                animator.SetFloat("Speed", 0);
                break;
            case PlayerState.MoveBackwards:
                movimientoLocal = new Vector3(0f, 0f, moveValue.y * velocidadHaciaAtras);
                animator.SetFloat("Speed", -1f);
                break;
            case PlayerState.Walk:
                movimientoLocal = new Vector3(0f, 0f, moveValue.y * velocidadAndando);
                animator.SetFloat("Speed", 0.2f);
                break;
            case PlayerState.Run:
                movimientoLocal = new Vector3(0f, 0f, moveValue.y * velocidadCorriendo);
                animator.SetFloat("Speed", 1f);
                break;
            case PlayerState.Jumping:
                animator.SetTrigger("Jump");
                movimientoLocal = new Vector3(0f, 0f, moveValue.y * (velocidadAndando * 0.5f));
                break;
            case PlayerState.Firing:
                animator.SetTrigger("Fire");
                break;
            case PlayerState.Saluting:
                animator.SetTrigger("Salute");
                break;
        }

        //  movement
        Vector3 movimientoMundo = transform.TransformDirection(movimientoLocal);
        movimientoMundo.y = velocidadVertical;
        characterController.Move(movimientoMundo * Time.deltaTime);
    }
}
