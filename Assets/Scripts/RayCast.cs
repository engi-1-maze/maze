using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class RayCast : MonoBehaviour
{
    Camera mainCamera;
    public float maxDistance = 10f;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Mouse.current == null) return;
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(ray.origin, ray.direction*maxDistance, Color.cyan, 1f);

        if(Physics.Raycast(ray,out RaycastHit hit,maxDistance,~0,QueryTriggerInteraction.Collide))
        {
            DoorButton doorButton=hit.collider.GetComponentInParent<DoorButton>();
            if(doorButton!=null) 
            {
                doorButton.Activate();
            }
            else { Debug.LogWarning($"Hit a hit.collider.name pero no hay DoorButton en el ni en sus padres");
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
}
