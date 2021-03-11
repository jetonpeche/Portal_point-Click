using UnityEngine;

public class DeplacementPorte : MonoBehaviour
{
    [SerializeField] private Transform[] listPoint;
    [SerializeField] private float vitesse;
    [SerializeField] private AudioClip sonPorte;

    public bool btnActiver;

    private bool sonJouer;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(btnActiver)
        {
            if(!sonJouer)
                audioSource.PlayOneShot(sonPorte);

            sonJouer = true;

            transform.position = Vector3.MoveTowards(transform.position, listPoint[1].position, vitesse * Time.deltaTime);
        }
        else
        {
            if (sonJouer)
                audioSource.PlayOneShot(sonPorte);

            sonJouer = false;

            transform.position = Vector3.MoveTowards(transform.position, listPoint[0].position, vitesse * Time.deltaTime);
        }
    }
}
