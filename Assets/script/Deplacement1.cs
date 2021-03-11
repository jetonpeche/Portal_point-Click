using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Deplacement1 : MonoBehaviour
{
    [SerializeField] private float vitesseRotationX, vitesseRotationY;
    [SerializeField] private float porterUtilisationTp;
    [SerializeField] private LayerMask layerTerrain, layerTp;
    [SerializeField] private Camera cam;
    [SerializeField] private float limiteCamRotationX;

    [SerializeField] private AudioClip sonPiedDroit;
    [SerializeField] private AudioClip sonPiedGauche;

    private bool jouerPiedDroit;
    private AudioSource audioSource;
    private float rotationActuelleCameraX = 0f;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // regarder en haut
        if (Input.GetKey(KeyCode.UpArrow))
        {
            RotationY(Vector3.left, vitesseRotationY);

            // bloque la camera en haut
            rotationActuelleCameraX -= 1;
            rotationActuelleCameraX = Mathf.Clamp(rotationActuelleCameraX, -limiteCamRotationX, limiteCamRotationX);
        }

        // regarde en bas
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            RotationY(Vector3.right, vitesseRotationY);

            // bloque la camera en bas
            rotationActuelleCameraX += 1;
            rotationActuelleCameraX = Mathf.Clamp(rotationActuelleCameraX, -limiteCamRotationX, limiteCamRotationX);
        }

        // regarder a gauche
        else if (Input.GetKey(KeyCode.LeftArrow))
            RotationX(Vector3.down, vitesseRotationX);

        // regarder a droite
        else if (Input.GetKey(KeyCode.RightArrow))
            RotationX(Vector3.up, vitesseRotationX);

        // force le Z a 0 pour ne pas avoir de truc space
        cam.transform.localEulerAngles = new Vector3(rotationActuelleCameraX, cam.transform.localEulerAngles.y, 0);


        // deplacer ou TP
        if (Input.GetMouseButtonDown(0))
        {
            Deplacer();
            UtiliserTeleporteur();
        }
    }

    private void Deplacer()
    {
        Ray _rayon = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit _hit;
        if (Physics.Raycast(_rayon, out _hit, Mathf.Infinity, layerTerrain, QueryTriggerInteraction.Ignore))
        {
            agent.SetDestination(_hit.point);

            if (IsInvoking(nameof(JouerSonMarcher)))
                CancelInvoke(nameof(JouerSonMarcher));

            InvokeRepeating(nameof(JouerSonMarcher), 0f, 0.4f);
        }
    }

    private void UtiliserTeleporteur()
    {
        Ray _rayon = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit _hit;
        if (Physics.Raycast(_rayon, out _hit, porterUtilisationTp, layerTp))
        {
            _hit.transform.GetComponent<Teleporteur>().Teleportation(gameObject);
        }
    }

    private void RotationY(Vector3 _rot, float _sensibilite)
    {
        cam.transform.Rotate(_rot * _sensibilite * Time.deltaTime);
    }

    private void RotationX(Vector3 _rot, float _sensibilite)
    {
        transform.Rotate(_rot * _sensibilite * Time.deltaTime);
        cam.transform.rotation = transform.rotation;
    }

    private void JouerSonMarcher()
    {
        if (Vector3.Distance(transform.position, agent.destination) >= 1.01)
        {
            if (!jouerPiedDroit)
            {
                audioSource.PlayOneShot(sonPiedDroit);
            }
            else
            {
                audioSource.PlayOneShot(sonPiedGauche);
            }

            jouerPiedDroit = !jouerPiedDroit;
        }
        else
        {
            CancelInvoke(nameof(JouerSonMarcher));
        }
    }
}
