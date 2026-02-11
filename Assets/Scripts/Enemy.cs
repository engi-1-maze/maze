using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject jugador;
    public float distanciaDeteccion = 1.5f;
    public float danioNormal = 25f;
    public float intervaloAtaque = 1.5f;
    private float tiempoSiguienteAtaque = 0f;

    [Range(0, 100)]
    public float probabilidadMuerteInstantanea = 15f;

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, jugador.transform.position);

        if (distancia < distanciaDeteccion && Time.time >= tiempoSiguienteAtaque)
        {
            LogicaAtaque();
            tiempoSiguienteAtaque = Time.time + intervaloAtaque;
        }
    }

    void LogicaAtaque()
    {
        float dado = Random.Range(0f, 100f);
        SaludJugador scriptSalud = jugador.GetComponent<SaludJugador>();

        if (scriptSalud == null) return;

        // OPCIÓN A: ¡ULTI! (Muerte directa)
        if (dado <= probabilidadMuerteInstantanea)
        {
            Debug.Log("¡ATAQUE DEFINITIVO!");
            IrAGameOver();
        }
        // OPCIÓN B: Ataque normal
        else
        {
            scriptSalud.RecibirDanio(danioNormal);

            // Si después del golpe normal te quedas sin vida... ¡GameOver también!
            if (scriptSalud.saludActual <= 0)
            {
                Debug.Log("El jugador se quedó sin vida.");
                IrAGameOver();
            }
        }
    }

    void IrAGameOver()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("GameOver");
        Invoke("CargarEscena2", 3f);
    }

    void CargarEscena2()
    {
        SceneManager.LoadScene("Scene2");
        Destroy(this.gameObject);
    }
}