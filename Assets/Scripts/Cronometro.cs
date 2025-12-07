using TMPro;
using UnityEngine;

public class Cronometro : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo;

    private float tiempo = 0f;
    private bool activo = false;
    void Update()
    {
        if (activo)
        {
            tiempo += Time.deltaTime;
            textoTiempo.text = "Tiempo: " + tiempo.ToString("F2");
        }
    }
    public void IniciarCronometro()
    {
        tiempo = 0f;
        activo = true;

    }
    public void PararCronometro()
    {
        activo = false;
    }
    public float Tiempo()
    {
        return tiempo;  
    }
}
