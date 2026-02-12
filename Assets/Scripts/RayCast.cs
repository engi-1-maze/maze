using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class RayCast : MonoBehaviour
{
    Camera mainCamera;
    public float maxDistance = 10f;
    public int damage = 1; // Daño al enemigo

    [Header("Sonido de disparo al enemigo")]
    public AudioSource audioDisparo; // Asigna en Inspector

    [Header("Prefab de láser")]
    public GameObject laserPrefab;    // Prefab que creamos (Cube fino con material Unlit)
    public float laserDuration = 0.05f; // Duración visible del láser

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current == null) return;
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.cyan, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, ~0, QueryTriggerInteraction.Collide))
        {
            // 🔹 Instanciar láser visible
            if (laserPrefab != null)
            {
                GameObject laserInstance = Instantiate(laserPrefab);
                laserInstance.SetActive(true);

                // Posicionar en la mitad entre cámara y hit
                laserInstance.transform.position = (ray.origin + hit.point) / 2f;

                // Apuntar al impacto
                laserInstance.transform.LookAt(hit.point);

                // Escalar en Z para que llegue al punto de impacto
                float distancia = Vector3.Distance(ray.origin, hit.point);
                laserInstance.transform.localScale = new Vector3(0.05f, 0.05f, distancia);

                // Destruir el láser después de un corto tiempo
                Destroy(laserInstance, laserDuration);
            }

            // PUERTAS
            DoorButton doorButton = hit.collider.GetComponentInParent<DoorButton>();
            if (doorButton != null)
            {
                doorButton.Activate();
            }
            else
            {
                Debug.LogWarning($"Hit a {hit.collider.name} pero no hay DoorButton en el ni en sus padres");
            }

            // ENEMIGOS
            Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                // 🔊 Reproducir sonido al golpear enemigo
                if (audioDisparo != null)
                    audioDisparo.Play();
            }
        }
        else
        {
            // Si no golpea nada, crear láser hasta distancia máxima
            if (laserPrefab != null)
            {
                GameObject laserInstance = Instantiate(laserPrefab);
                laserInstance.SetActive(true);

                laserInstance.transform.position = ray.origin + ray.direction * maxDistance / 2f;
                laserInstance.transform.LookAt(ray.origin + ray.direction * maxDistance);
                laserInstance.transform.localScale = new Vector3(0.1f, 0.1f, maxDistance);

                Destroy(laserInstance, laserDuration);
            }
        }
    }
}


    /*
    RaycastHit hit;
    void Update()
    {
        if(Mouse.current.leftButton.isPressed)
        {

            Ray ray= mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            Debug.Log(Mouse.current.position.ReadValue());
            Debug.DrawRay(ray.origin, ray.direction * 20f, Color.royalBlue);
     

            if (Physics.Raycast(ray,out hit,maxDistance))
            {
               if(hit.collider.TryGetComponent(out DoorButton doorButton))
                {
                    doorButton.Activate();
             
                }
            }

        }
    }*/

