using UnityEngine;
using UnityEngine.SceneManagement; // �Importante para cargar el GameOver!

public class Trap : MonoBehaviour
{
    public float danio = 25f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SaludJugador scriptSalud = other.GetComponent<SaludJugador>();
            if (scriptSalud != null)
            {
                scriptSalud.RecibirDanio(danio);
            }
        }
    }
}