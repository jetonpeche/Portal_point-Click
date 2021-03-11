using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private Camera cam;
    [SerializeField] private Text text;

    private GameObject cube;
    private BoutonConsole boutonConsole;
    private BoutonConsoleTempo boutonConsoleTempo;

    private bool peutSaisir;
    private bool peutAppuyer;
    
    private void Update()
    {
        Check();

        if(Input.GetKeyDown(KeyCode.A))
        {
            // ramasser cube
            if(!peutAppuyer)
            {
                if (peutSaisir)
                    RamasserCube();
                else
                    Lacher();
            }
            // appuyer btn
            else if (peutAppuyer)
            {
                AppuyerBtn();
            }
        }
    }

    private void Check()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, distance))
        {
            if (_hit.transform.CompareTag("cubeBleu"))
            {
                boutonConsole = null;

                peutSaisir = true;
                cube = _hit.transform.gameObject;
                text.text = "[A] Ramasser le cube";
            }
            else if(_hit.transform.CompareTag("btn"))
            {
                InteractionBtn();
                boutonConsole = _hit.transform.GetComponent<BoutonConsole>();
            }
            else if(_hit.transform.CompareTag("btnPorteTempo"))
            {
                InteractionBtn();
                boutonConsoleTempo = _hit.transform.GetComponent<BoutonConsoleTempo>();
            }
            else
            {
                Rien();
            }
        }
        else
        {
            Rien();
        }
    }

    private void RamasserCube()
    {
        Inventaire.instance.AjouterCube();
        Destroy(cube);

        cube = null;
    }

    private void Lacher()
    {
        Inventaire.instance.InstanceCube();
        cube = null;
    }

    private void InteractionBtn()
    {
        cube = null;
        peutSaisir = false;

        text.text = "[A] pour interragir";
        peutAppuyer = true;
    }

    private void Rien()
    {
        boutonConsoleTempo = null;
        boutonConsole = null;
        cube = null;

        peutAppuyer = false;
        text.text = "";
        peutSaisir = false;
    }

    private void AppuyerBtn()
    {
        if (boutonConsole != null)
            boutonConsole.CliquerBtn();
        else
            boutonConsoleTempo.OuvrirPorteTemporairement();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * distance);
    }
}
