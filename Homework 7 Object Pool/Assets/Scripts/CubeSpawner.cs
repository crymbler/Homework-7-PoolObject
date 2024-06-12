using UnityEngine;

public class CubeSpawner : MonoBehaviour, ISpawnerReceiver
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private float _minSpawnPositionX;
    [SerializeField] private float _maxSpawnPositionX;
    [SerializeField] private float _minSpawnPositionZ;
    [SerializeField] private float _maxSpawnPositionZ;

    private void FixedUpdate()
    {
        if (_objectPool.TryGetObject(out Cube cube) == false)
        {
            return;
        }

        cube.Initialize(this);
        cube.transform.position = new Vector3(Random.Range(_minSpawnPositionX, _maxSpawnPositionX),
                                                transform.position.y, Random.Range(_minSpawnPositionZ, _maxSpawnPositionZ));
    }

    public void OnLifetimeEnded(Cube cube)
    {
        _objectPool.ReturnObject(cube);
    }
}