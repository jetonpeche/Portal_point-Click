using UnityEngine;

public class PiegeSol : MonoBehaviour
{
    private Rigidbody[] listRb;
    private bool piegeActiver;

    private void Start()
    {
        listRb = GetComponentsInChildren<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !piegeActiver)
        {
            piegeActiver = true;

            foreach (Rigidbody _rb in listRb)
            {
                _rb.useGravity = true;
                _rb.transform.Rotate(new Vector3(Aleatoire(), Aleatoire(), Aleatoire()));
            }

            Destroy(gameObject, 5f);
        }
    }

    private float Aleatoire()
    {
        return Random.Range(0, 360f);
    }
}
