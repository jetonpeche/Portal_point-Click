using UnityEngine;

public class DetruireCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Inventaire.instance.ResetInventaireCube();
        }
    }
}
