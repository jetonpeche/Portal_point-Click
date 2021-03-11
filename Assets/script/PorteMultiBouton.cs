using UnityEngine;

[RequireComponent(typeof(DeplacementPorte))] 
public class PorteMultiBouton : MonoBehaviour
{
    [SerializeField] private DeclancherBoutton[] listBouton;

    void Start()
    {
        InvokeRepeating("VerifBoutonActiver", 0f, 0.2f);
    }

    private void VerifBoutonActiver()
    {
        int _nbBtnActiver = 0;

        for (int i = 0; i < listBouton.Length; i++)
        {
            if(listBouton[i].GetBtnActiver())
                _nbBtnActiver += 1;
            else
                break;
        }

        // controle de la porte
        if(_nbBtnActiver == listBouton.Length)
            GetComponent<DeplacementPorte>().btnActiver = true;
        else
            GetComponent<DeplacementPorte>().btnActiver = false;
    }
}
