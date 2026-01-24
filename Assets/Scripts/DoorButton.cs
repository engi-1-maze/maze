using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private Button button;
    public void Activate()
    {
        if (button != null) button.SetActive();

        if(door!=null)door.TriggerOpen();
    }


}
