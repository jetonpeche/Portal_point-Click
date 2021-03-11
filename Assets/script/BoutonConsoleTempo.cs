using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BoutonConsoleTempo : MonoBehaviour
{
    [SerializeField] private int tempo;
    [SerializeField] private DeplacementPorte deplacementPorte = null;

    private bool btnAppuyer;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OuvrirPorteTemporairement()
    {
        if (!btnAppuyer)
            StartCoroutine(Tempo());
    }

    IEnumerator Tempo()
    {
        audioSource.Play();
        btnAppuyer = true;
        deplacementPorte.btnActiver = true;
        yield return new WaitForSeconds(tempo);
        deplacementPorte.btnActiver = false;
        btnAppuyer = false;
        audioSource.Stop();
    }
}
