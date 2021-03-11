using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class BoutonConsole : MonoBehaviour
{
    [SerializeField] private SpawnerCube spawnerCube = null;
    [SerializeField] private OffMeshLink offMesh = null;
    [SerializeField] private AudioClip son;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void CliquerBtn()
    {
        spawnerCube.SpawnCube();
        audioSource.PlayOneShot(son);

        if(offMesh != null && !offMesh.activated)
        {
            offMesh.activated = true;
        }
    }
}
