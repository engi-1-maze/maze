using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform salida;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("algo entro al trigger"+other.name);
            if(other.CompareTag("Player")||other.CompareTag("Enemy"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();
            if(controller != null) controller.enabled = false;

            Debug.Log("es el jugador teletransportando");
            other.transform.position = salida.position;
            if (controller != null) controller.enabled = true;
        }
    }
}
