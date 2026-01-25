using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadMovimiento = 5f;
    public float velocidadGiro = 120f;
    private float velocidadVertical = 0f;
    public float gravedad = 9.8f;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Input del jugador
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        // Giro
        float anguloGiro = inputHorizontal * velocidadGiro * Time.deltaTime;
        transform.Rotate(0f, anguloGiro, 0f);

        // Movimiento personaje en espacio local
        Vector3 movimientoLocal = new Vector3(0f, 0f, inputVertical * velocidadMovimiento);

        // Conversion de local a global
        Vector3 movimientoMundo = transform.TransformDirection(movimientoLocal);

        //gravedad
        if(characterController.isGrounded)
        {
            velocidadVertical = -0.5f;
        }
        else
        {
            velocidadVertical-=gravedad*Time.deltaTime;
        }
        movimientoMundo.y=velocidadVertical;

        //Movimiento player
        characterController.Move(movimientoMundo * Time.deltaTime);
    }
}
