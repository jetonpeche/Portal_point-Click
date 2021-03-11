using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public class Deplacement : MonoBehaviour
{
    [SerializeField] private float vitesseRotationX, vitesseRotationY;
    [SerializeField] private float porterUtilisationTp;
    [SerializeField] private LayerMask layerTerrain, layerTp;
    [SerializeField] private Camera cam;
    [SerializeField] private float limiteCamRotationX;

    [SerializeField] private AudioClip sonPiedDroit;
    [SerializeField] private AudioClip sonPiedGauche;

    // pour que le multi-aim constrainte fonctionne
    [SerializeField] private Transform refCamPosition;

    private float rotationActuelleCameraX = 0f;
    private NavMeshAgent agent;
    private bool jouerPiedDroit;
    private AudioSource audioSource;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // regarder en haut
        if (Input.GetKey(KeyCode.UpArrow))
        {
            RotationY(Vector3.left, vitesseRotationY);

            // bloque la camera en haut
            rotationActuelleCameraX -= 1;
            rotationActuelleCameraX = Mathf.Clamp(rotationActuelleCameraX, -limiteCamRotationX, 40);
        }

        // regarde en bas
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            RotationY(Vector3.right, vitesseRotationY);

            // bloque la camera en bas
            rotationActuelleCameraX += 1;
            rotationActuelleCameraX = Mathf.Clamp(rotationActuelleCameraX, -limiteCamRotationX, 40);
        }

        // regarder a gauche
        else if (Input.GetKey(KeyCode.LeftArrow))
            RotationX(Vector3.down, vitesseRotationX);

        // regarder a droite
        else if (Input.GetKey(KeyCode.RightArrow))
            RotationX(Vector3.up, vitesseRotationX);

        // force le Z a 0 pour ne pas avoir de truc space
        refCamPosition.localEulerAngles = new Vector3(rotationActuelleCameraX, refCamPosition.transform.localEulerAngles.y, 0);   

        // deplacer ou TP
        if (Input.GetMouseButtonDown(0))
        {
            Deplacer();
            UtiliserTeleporteur();
        }

        cam.transform.position = refCamPosition.position;

        animator.SetFloat("vitesse", agent.velocity.magnitude);
    }

    private void Deplacer()
    {
        Ray _rayon = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit _hit;
        if (Physics.Raycast(_rayon, out _hit, Mathf.Infinity, layerTerrain, QueryTriggerInteraction.Ignore))
        {
            agent.destination = _hit.point;

            if (IsInvoking(nameof(JouerSonMarcher)))
                CancelInvoke(nameof(JouerSonMarcher));

            InvokeRepeating(nameof(JouerSonMarcher), 0f, 0.3f);
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
       refCamPosition.Rotate(_rot * _sensibilite * Time.deltaTime);
        PosRotationCam();
    }

    private void RotationX(Vector3 _rot, float _sensibilite)
    {
        // evite que le perso tourne en rond quand il marche
        if (agent.velocity.magnitude < 0.3f)
        {
            transform.Rotate(_rot * _sensibilite * Time.deltaTime);
            PosRotationCam();
            refCamPosition.rotation = transform.rotation;
        }
    }

    private void PosRotationCam()
    {
        cam.transform.rotation = refCamPosition.rotation;
    }

    private void JouerSonMarcher()
    {
        if(Vector3.Distance(transform.position, agent.destination) >= 1.01)
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
