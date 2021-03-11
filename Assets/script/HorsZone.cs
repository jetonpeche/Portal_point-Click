using UnityEngine;

public class HorsZone : MonoBehaviour
{
    private Transform ptSpawn;

    private void Start()
    {
        ptSpawn = transform.GetChild(0).transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("cubeBleu"))
        {
            other.transform.position = ptSpawn.position;
        }
    }
}
