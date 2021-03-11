using UnityEngine;

public class VoixOff : MonoBehaviour
{
    [SerializeField] private AudioClip son;

    private AudioSource audioSource;
    private bool audioJouer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = son;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !audioJouer)
        {  
            audioSource.Play();
            audioJouer = true;
        }
    }
}
