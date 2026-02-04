using System;
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

    void LateUpdate()
    {
        Vector3 desiredPosition;
        if (currentMode == CameraType.FirstPerson)
        {
            Vector3 basePos = target.position;
            Vector3 eyeLevel = new Vector3(0, 1.8f, 0);
            transform.position = basePos + eyeLevel;

            float playerYRotation = target.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, playerYRotation, 0);
        }
        else
        {
            // In TP, we use the smooth lag follow
            desiredPosition = target.position + tpOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(target.position + Vector3.up * 1.5f); // Look slightly above feet
        }

    }

    public void ToggleCamera()
    {
        currentMode = (currentMode == CameraType.FirstPerson) ? CameraType.ThirdPerson : CameraType.FirstPerson;
    }
}
