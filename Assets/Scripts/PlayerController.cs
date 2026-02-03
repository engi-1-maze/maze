using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState{Idle, Walking, Jumping,Firing,Saluting}
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadMovimiento = 5f;
    public float velocidadGiro = 120f;
    private float velocidadVertical = 0f;
    public float gravedad = 9.8f;

    private CharacterController characterController;
    [SerializeField] private GameObject model;
    private Animator animator;
    private PlayerState currentState = PlayerState.Idle;


    private PlayerInput playerInput;

    // Actions
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction fireAction;
    private InputAction saluteAction;

    // Input Storage
    private Vector2 moveValue;
    private bool jumpPressed;
    private bool firePressed;
    private bool salutePressed;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (model == null)
        {
            Debug.LogError("No model selected");
            return;
        }
        animator = model.GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("No model selected");
            return;
        }

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        fireAction = playerInput.actions["Fire"];
        saluteAction = playerInput.actions["Salute"];
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
    }

    private void updateState(){
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool isDoingAction = stateInfo.IsName("Salute") || stateInfo.IsName("Jumping") || stateInfo.IsName("Firing Rifle");
        
        if (isDoingAction && stateInfo.normalizedTime < 1.0f) {
            return; 
        }
        
        // 3. Normal State Switching
        if (firePressed) currentState = PlayerState.Firing;
        else if (salutePressed) currentState = PlayerState.Saluting;
        else if (jumpPressed) currentState = PlayerState.Jumping;
        else if (moveValue.magnitude > 0.1f) currentState = PlayerState.Walking;
        else currentState = PlayerState.Idle;
    }

    private void updateAction(){
        // Rotate
        float anguloGiro = moveValue.x * velocidadGiro * Time.deltaTime;
        transform.Rotate(0f, anguloGiro, 0f);

        // Gravity
        if (characterController.isGrounded) {
            velocidadVertical = -0.5f; 
        } else {
            velocidadVertical -= gravedad * Time.deltaTime;
        } 
        // Move
        Vector3 movimientoLocal = Vector3.zero;

        switch (currentState){
            case PlayerState.Idle:
                animator.SetFloat("Speed", 0);
                break;
            case PlayerState.Walking:
                movimientoLocal = new Vector3(0f, 0f, moveValue.y * velocidadMovimiento);
                animator.SetFloat("Speed", Mathf.Abs(moveValue.y));
                break;
            case PlayerState.Jumping:
                animator.SetTrigger("Jump");
                movimientoLocal = new Vector3(0f, 0f, moveValue.y * (velocidadMovimiento * 0.5f));
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
