using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class voixOffObjectif : MonoBehaviour
{
    [SerializeField] private DeclancherBoutton declancherBoutton;
    [SerializeField] private AudioClip son;

    private AudioSource audioSource;
    private bool audioJouer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(declancherBoutton.GetBtnActiver() && !audioJouer)
        {
            audioJouer = true;
            audioSource.PlayOneShot(son);
        }
    }
}
