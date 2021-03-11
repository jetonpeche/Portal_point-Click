using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PortalGun : MonoBehaviour
{
    #region variables

    [SerializeField] private GameObject tp1;
    [SerializeField] private GameObject tp2;
    [SerializeField] private string tagMurBlanc;
    [SerializeField] private Camera cam;

    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem effectHit;

    [SerializeField] private AudioClip sonTir;
    [SerializeField] private AudioClip sonTirPortail;

    private AudioSource audioSource;

    private bool tp1Poser;
    private bool tp2Poser;

    private GameObject premierTpPoser;
    private GameObject dernierTpPoser;
    #endregion

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Ray _rayon = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit _hit;
            GameObject _obj;

            if(Physics.Raycast(_rayon, out _hit, Mathf.Infinity))
            {
                muzzleFlash.Emit(1);

                if (_hit.transform.CompareTag(tagMurBlanc))
                {
                    // premiere utilisation des portails
                    if (!tp1Poser)
                    {
                        CreerTp(tp1, _hit, out _obj);

                        tp1Poser = true;
                        premierTpPoser = _obj;
                    }
                    else if (!tp2Poser)
                    {
                        CreerTp(tp2, _hit, out _obj);

                        dernierTpPoser = _obj;
                        tp2Poser = true;
                    }

                    // portail deja spawn
                    else if (premierTpPoser.CompareTag("tp1"))
                    {
                        Destroy(premierTpPoser);

                        CreerTp(tp1, _hit, out _obj);

                        premierTpPoser = dernierTpPoser;
                        dernierTpPoser = _obj;
                    }
                    else if (premierTpPoser.CompareTag("tp2"))
                    {
                        Destroy(premierTpPoser);

                        CreerTp(tp2, _hit, out _obj);

                        premierTpPoser = dernierTpPoser;
                        dernierTpPoser = _obj;
                    }
                }
                else
                {
                    // placement et jouer l'effect hit
                    effectHit.transform.position = _hit.point;
                    effectHit.transform.forward = _hit.normal;
                    effectHit.Emit(1);

                    audioSource.PlayOneShot(sonTir);
                }
            }
        }
    }

    private void CreerTp(GameObject _tp, RaycastHit _hit, out GameObject _obj)
    {
        audioSource.PlayOneShot(sonTirPortail);

        _obj = Instantiate(_tp);
        _obj.transform.position = _hit.point;
        _obj.transform.forward = _hit.normal;
    }
}
