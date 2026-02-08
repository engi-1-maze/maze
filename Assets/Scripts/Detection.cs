using System.Collections;
using TMPro;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public TextMeshProUGUI detectText;
    public float seconds = 2f;

    Coroutine c;

    private void Start()
    {
        if(detectText!=null)
            detectText.gameObject.SetActive(false); 
    }

    public void Show(string msg)
    {
        if (detectText == null) return;

        if (c != null) StopCoroutine(c);
        c = StartCoroutine(Routine(msg));
    }

    IEnumerator Routine(string msg)
    {
        detectText.gameObject.SetActive(true);
        detectText.text = msg;
        yield return new WaitForSeconds(seconds);

        detectText.gameObject.SetActive(false);
    }



}
