using UnityEngine;
using UnityEngine.SceneManagement;

public class FinJeu : MonoBehaviour
{
    [SerializeField] AudioSource audio = null;

    private bool fin;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            fin = true;
        }
    }

    private void Update()
    {
        if(!audio.isPlaying && fin)
        {
            SceneManager.LoadScene("fin");
        }
    }
}
