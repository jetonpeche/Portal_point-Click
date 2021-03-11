using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu = null;

    private bool pause;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pause)
            {
                pause = true;
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                AudioListener.pause = true;
            }
            else
            {
                Continuer();
            }
        }
    }

    public void Continuer()
    {
        AudioListener.pause = false;
        pause = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void MenuPrincipal()
    {
        Continuer();
        SceneManager.LoadScene("menuPrincipal");
    }

    public void Quitter()
    {
        Application.Quit();
    }
}
