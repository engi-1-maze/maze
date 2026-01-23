using UnityEngine;

public class MapController : MonoBehaviour
{
    public Transform personaje;
    public bool girarConPersonaje;
 
    void LateUpdate()
    {
        transform.position = new Vector3(personaje.position.x, transform.position.y, personaje.position.z);

        if(girarConPersonaje)
        {
            transform.eulerAngles = new Vector3(90,0,personaje.transform.eulerAngles.y);
        }
    }
}
