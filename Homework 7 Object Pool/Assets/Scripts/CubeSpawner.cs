using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour, ISpawnerReceiver
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private float _minSpawnPositionX;
    [SerializeField] private float _maxSpawnPositionX;
    [SerializeField] private float _minSpawnPositionZ;
    [SerializeField] private float _maxSpawnPositionZ;
    [SerializeField] private float _spawnDelay;

    private void Start()
    {
        StartCoroutine(SpawnCube());
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnCube());
    }

    private IEnumerator SpawnCube()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnDelay);

            if (_objectPool.TryGetObject(out Cube cube) == true)
            {
                cube.Initialize(this);
                cube.transform.position = new Vector3(Random.Range(_minSpawnPositionX, _maxSpawnPositionX),
                                                        transform.position.y, Random.Range(_minSpawnPositionZ, _maxSpawnPositionZ));
            }
        }
    }

    public void ReturnToPool(Cube cube)
    {
        _objectPool.ReturnObject(cube);
    }
}