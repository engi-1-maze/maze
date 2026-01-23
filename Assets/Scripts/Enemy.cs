using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject jugador;
    float distancia;

    void Update()
    {
        distancia= Vector3.Distance(transform.position, jugador.transform.position);

        if (distancia<1)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
