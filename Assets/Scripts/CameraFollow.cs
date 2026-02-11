using UnityEngine;

public enum CameraType { FirstPerson, ThirdPerson }

public class CameraFollow : MonoBehaviour
{

    public CameraType currentMode = CameraType.ThirdPerson;
    public Transform target;

    [Header("Third Person Settings")]
    public Vector3 tpOffset = new Vector3(0, 5, -10);

    [Header("First Person Settings")]
    public Vector3 fpOffset = new Vector3(0, 0.85f, 0.2f);
    public float smoothSpeed = 0.125f;




    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Camera has no target");
        }
    }

    void OnEnable()
    {
        target.GetComponent<PlayerController>().OnCameraToggleRequested += ToggleCamera;
    }

    void Osable()
    {
        target.GetComponent<PlayerController>().OnCameraToggleRequested -= ToggleCamera;       
    }

    void LateUpdate()
    {
        if (currentMode == CameraType.FirstPerson)
        {
            Vector3 basePos = target.position;
            Vector3 eyeLevel = target.TransformDirection(fpOffset); 
            transform.position = basePos + eyeLevel;
            transform.rotation = target.rotation;
        }
        else
        {
            // In TP, we use the smooth lag follow
            Vector3 rotatedOffset = target.TransformDirection(tpOffset);
            Vector3 desiredPosition = target.position + rotatedOffset;

            // 2. Smoothly follow that position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            // 3. Look at the player (slightly above their feet)
            transform.LookAt(target.position + Vector3.up * 1.5f);
        }

    }

    public void ToggleCamera()
    {
        currentMode = (currentMode == CameraType.FirstPerson) ? CameraType.ThirdPerson : CameraType.FirstPerson;
    }
}
