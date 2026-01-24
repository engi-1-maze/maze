using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float down = 5f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float time = 2f;

    [SerializeField] private Button button1;
    [SerializeField] private Button button2;

    private Vector3 startPos;
    private Vector3 endPos;

    private Coroutine routine;
    private void Awake()
    {
        startPos = transform.position;
        endPos = startPos+Vector3.down*down;
    }

    public void TriggerOpen()
    {
        if(routine!=null)
            StopCoroutine(routine);

        if (button1 != null)
            button1.SetActive();
        if (button2 != null)
            button2.SetActive();

        routine = StartCoroutine(OpenCloseRoutine());
    }
    private IEnumerator
        OpenCloseRoutine()
    {
        yield return MoveTo(endPos);
        if (button1 != null)
            button1.SetInactive();
        if (button2 != null)
            button2.SetInactive();
        yield return new WaitForSeconds(time);
        yield return MoveTo(startPos);

        routine = null;

    }
    private IEnumerator MoveTo(Vector3 target)
    {
        while((transform.position-target).sqrMagnitude>0.0001f)
            {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
    }
}
