using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesHandler : MonoBehaviour
{

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

    public void IrAMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(this.gameObject);
    }
    public void IrACreditos()
    {
        SceneManager.LoadScene("Creditos");
        Destroy(this.gameObject);
    }
}
