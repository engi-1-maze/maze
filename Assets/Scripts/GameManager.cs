using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    public Cronometro cronometro;
    public TextMeshProUGUI textoLlegada;
    public Button play;

    [Header("Entrada")]
    public GameObject puertaEntrada;
    public Collider triggerCierre;

    [Header("Salida")]
    public Collider triggerSalida;

    Vector3 posicionInicial;
    Quaternion rotacionInicial;
    
    void Start()
    {

        if (player != null)
        {
            posicionInicial = player.position;
            rotacionInicial = player.rotation;
        }
        ReiniciarJuego();
    }
    public void ReiniciarJuego()
    {
        //Muevo jugador al inicio
        if (player != null)
        {
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            player.position = posicionInicial;
            player.rotation = rotacionInicial;

            if (cc != null) cc.enabled = true;
        }

        // Desactivo el texto de llegada
        if (textoLlegada != null)
        {
            textoLlegada.gameObject.SetActive(false);
            textoLlegada.text = "";
        }

        //Desactivo el bot�n 
        if(play!=null)
        {
            play.gameObject.SetActive(false);
        }
        // Reseteo el cron�metro
        if (cronometro != null)
        {
            cronometro.IniciarCronometro();
        }
        // Abro la puerta
        if (puertaEntrada != null)
        {
            puertaEntrada.SetActive(false);
        }

        // Activo el trigger de entrada que cierra la puerta
        if (triggerCierre != null)
        {
            triggerCierre.enabled = true;
        }

        // Activo trigger de salida
        if (triggerSalida != null)
        {
            triggerSalida.enabled = true;
        }

    }
}
