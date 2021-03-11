using UnityEngine;
using UnityEngine.AI;

public class Teleporteur : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.CompareTag("cubeBleu"))
        {
            GameObject _destination;
            ChercheTp(out _destination);

            if (_destination != null)
                collision.transform.position = _destination.transform.position + _destination.transform.forward;      
        }
    }

    // click sur le portail
    public void Teleportation(GameObject _joueur)
    {
        GameObject _destination;
        ChercheTp(out _destination);

        if (_destination != null)
        {
            _joueur.transform.position = _destination.transform.position + _destination.transform.forward *1.5f;

            // permet de TP le joueur
            _joueur.transform.GetComponent<NavMeshAgent>().Warp(_joueur.transform.position);
            _joueur.transform.eulerAngles = _destination.transform.eulerAngles;
        }
    }

    private void ChercheTp(out GameObject _tp)
    {
        if (gameObject.CompareTag("tp1"))
        {
            _tp = GameObject.FindGameObjectWithTag("tp2");
        }
        else
        {
            _tp = GameObject.FindGameObjectWithTag("tp1");
        }
    }
}
