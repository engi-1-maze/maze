using UnityEngine;
using UnityEngine.SceneManagement; // ¡Importante para cargar el GameOver!

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
                // 1. Aplicamos el daño
                scriptSalud.RecibirDanio(danio);

                // 2. Comprobamos si el jugador ha perdido toda la vida
                if (scriptSalud.saludActual <= 0)
                {
                    Debug.Log("Vida agotada por trampa. Cargando GameOver...");
                    IrAGameOver();
                }
            }
        }
    }

    // Usamos la misma lógica que en el enemigo para que sea consistente
    void IrAGameOver()
    {
        // Desvinculamos para que no haya errores al destruir la escena
        transform.SetParent(null);

        // Mantenemos este objeto vivo para que el Invoke funcione
        DontDestroyOnLoad(this.gameObject);

        SceneManager.LoadScene("GameOver");

        // A los 3 segundos volvemos al juego
        Invoke("CargarEscena2", 3f);
    }

    void CargarEscena2()
    {
        SceneManager.LoadScene("Scene2");
        Destroy(this.gameObject);
    }
}