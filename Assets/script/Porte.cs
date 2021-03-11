using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Porte : MonoBehaviour
{
    [SerializeField] private Transform[] listPoint;
    [SerializeField] private Transform porte;
    [SerializeField] private float vitesse;
    [SerializeField] private AudioClip sonPorte;

    private int index;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Vector3.Distance(porte.position, listPoint[index].position) > 0.01)
        {
            porte.position = Vector3.MoveTowards(porte.position, listPoint[index].position, vitesse * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            index = 1;
            JouerSonPorte();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            index = 0;
            JouerSonPorte();
        }
    }

    private void JouerSonPorte()
    {
        audioSource.PlayOneShot(sonPorte);
    }
}
