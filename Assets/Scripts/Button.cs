using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    [SerializeField] private Color activeColor = Color.green;
    [SerializeField] private Color inactiveColor = Color.red;

    private void Awake()
    {
        if(rend==null)
            rend=GetComponent<Renderer>();
        
        SetInactive();
    }

    public void SetActive()
    {
        rend.material = new Material(rend.material);
        rend.material.color = activeColor;
    }
    public void SetInactive()
    {
        rend.material = new Material(rend.material);
        rend.material.color = inactiveColor;
    }
}
