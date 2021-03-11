using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Jouer()
    {
        SceneManager.LoadScene("jeu");
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("menuPrincipal");
    }

    public void Quitter()
    {
        Application.Quit();
    }
}
