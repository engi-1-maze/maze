using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class SaludJugador : MonoBehaviour
{
    public float saludMaxima = 100f;
    public float saludActual;
    public TextMeshProUGUI textoVida;
    public UnityEvent onPlayerDeath;

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
        checkLife();
    }

    public void MuerteInstantanea()
    {
        saludActual = 0;
        checkLife();
    }

    public void checkLife()
    {
        if(saludActual > 0) return;
        onPlayerDeath.Invoke();
    }
    public void ActualizarInterfaz()
    {
        if (textoVida != null)
        {
            textoVida.text = "Vida: " + saludActual.ToString() + "%";
        }
    }
}