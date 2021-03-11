using UnityEngine;

public class DeclancherBoutton : MonoBehaviour
{
    [SerializeField] private GameObject porte;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float radiusDetection;
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private Material[] listMat;
    [SerializeField] private bool porteMultiBouton;
    [SerializeField] private AudioSource audioSource;

    private Animation anim;
    private AnimationClip clipAppuyer;
    private AnimationClip clipRelacher;

    private bool btnActiver;
    private bool stop;

    private void Start()
    {
        anim = GetComponentInParent<Animation>();

        clipAppuyer = anim.GetClip("apuyer");
        clipRelacher = anim.GetClip("relacher");

        lineRenderer.colorGradient.mode = GradientMode.Fixed;
        lineRenderer.material = listMat[0];

        InvokeRepeating("Detection", 0f, 0.4f);
    }

    private void Detection()
    {
        Collider[] listObj = Physics.OverlapSphere(transform.position, radiusDetection, layer);

        // rien sur le bouton
        if (listObj.Length == 0 && stop)
        {
            AppuyerBouton(false, clipRelacher, 0);
        }
        // bouton appuyer
        else if (listObj.Length > 0 && !btnActiver)
        {
            AppuyerBouton(true, clipAppuyer, 1);
        }
    }

    public bool GetBtnActiver()
    {
        return btnActiver;
    }

    private void AppuyerBouton(bool _etat, AnimationClip _animClip, int _indexMat)
    {
        if (!porteMultiBouton)
            porte.GetComponent<DeplacementPorte>().btnActiver = _etat;

        btnActiver = _etat;
        stop = _etat;

        lineRenderer.material = listMat[_indexMat];

        audioSource.Play();
        anim.Play(_animClip.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radiusDetection);
    }
}
