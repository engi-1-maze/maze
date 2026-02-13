using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public Cronometro cronometro;        // referencia al script del cron�metro
    public TextMeshProUGUI textoLlegada; // texto del letrero final
    public UnityEngine.UI.Button play;

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("Player"))
        {
            // Parar el cron�metro y obtener el tiempo
            float tiempoTotal = 0f;
            if (cronometro != null)
            {
                cronometro.PararCronometro();
                tiempoTotal = cronometro.Tiempo();
            }
            // Mostrar el letrero
            if (textoLlegada != null)
            {
                textoLlegada.gameObject.SetActive(true);
                textoLlegada.text = "��Llegaste!!\nHas tardado " + tiempoTotal.ToString("F2") + " segundos";
                play.gameObject.SetActive(true);
               
            }
        }
    }
}
