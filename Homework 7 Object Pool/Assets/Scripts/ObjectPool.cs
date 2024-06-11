using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _poolCapacity;

    private Queue<Cube> _pool;

    private void Awake()
    {
        _pool = new Queue<Cube>();
    }

    private void Start()
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            Cube instanceObject = Instantiate(_prefab);

            instanceObject.gameObject.SetActive(false);
            _pool.Enqueue(instanceObject);
        }
    }

    public bool TryGetObject()
    {
        if (_pool.Count == 0)
            return false;

        return true;
    }

    public void ReturnObject(Cube returnedObject)
    {
        returnedObject.gameObject.SetActive(false);
        _pool.Enqueue(returnedObject);
    }

    public Cube GetObject()
    {
        Cube takenObject = _pool.Dequeue();
        takenObject.gameObject.SetActive(true);

        return takenObject;
    }
}