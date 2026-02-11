using UnityEngine;
using TMPro;

public class SaludJugador : MonoBehaviour
{
    public float saludMaxima = 100f;
    public float saludActual;
    public TextMeshProUGUI textoVida;

    void Start()
    {
        saludActual = saludMaxima;
        ActualizarInterfaz();
    }

    public void RecibirDanio(float cantidad)
    {
        saludActual -= cantidad;
        if (saludActual < 0) saludActual = 0; // Para que no salga vida negativa

        ActualizarInterfaz();
    }

    public void ActualizarInterfaz()
    {
        if (textoVida != null)
        {
            textoVida.text = "Vida: " + saludActual.ToString() + "%";
        }
    }
}