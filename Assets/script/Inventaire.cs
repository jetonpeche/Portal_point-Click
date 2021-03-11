using UnityEngine;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour
{
    #region Singleton
    public static Inventaire instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject cube = null;
    [SerializeField] private Text txtCube;

    private int nbCube = 0;

    public void InstanceCube()
    {
        if(nbCube > 0)
        {
            RetirerCube(1);
            Instantiate(cube, transform.position + transform.forward, Quaternion.identity);
            MajCompteurCube();
        }
    }

    public void AjouterCube()
    {
        nbCube++;
        MajCompteurCube();
    }

    public void RetirerCube(int _nb)
    {
        nbCube -= _nb;

        if (nbCube < 0)
            nbCube = 0;

        MajCompteurCube();
    }

    public void ResetInventaireCube()
    {
        nbCube = 0;
        MajCompteurCube();
    }

    private void MajCompteurCube()
    {
        txtCube.text = nbCube.ToString();
    }
}
