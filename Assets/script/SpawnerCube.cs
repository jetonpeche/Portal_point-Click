using UnityEngine;

public class SpawnerCube : MonoBehaviour
{
    [SerializeField] private GameObject cube;

    private GameObject cubeSpawn = null;

    public void SpawnCube()
    {
        if(cubeSpawn == null)
        {
            GameObject _obj = Instantiate(cube, transform.position, Quaternion.identity);
            cubeSpawn = _obj;
        }
    }
}
