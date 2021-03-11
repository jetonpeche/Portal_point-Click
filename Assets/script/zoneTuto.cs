using UnityEngine;
using UnityEngine.UI;

public class zoneTuto : MonoBehaviour
{
    [SerializeField] private Text txt_tuto = null;
    [SerializeField] [TextArea] private string txt;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            txt_tuto.text = txt;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            txt_tuto.text = "";
        }
    }
}
