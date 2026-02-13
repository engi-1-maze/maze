using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void IrAJuego()
    {
        SceneManager.LoadScene("Scene2");
        Destroy(this.gameObject);
    }
    public void IrAGameOver()
    {
        SceneManager.LoadScene("GameOver");
        Destroy(this.gameObject);
    }
}
