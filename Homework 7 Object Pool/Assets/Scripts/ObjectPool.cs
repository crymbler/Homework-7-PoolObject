using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;

    private Queue<Cube> _pool;

    private void Awake()
    {
        _pool = new Queue<Cube>();
    }

    private void Start()
    {
        Cube instanceObject = CreateObject(_prefab);
        instanceObject.gameObject.SetActive(false);

        _pool.Enqueue(instanceObject);
    }

    public bool TryGetObject(out Cube cube)
    {
        if (_pool.Count > 0)
        {
            cube = GetObject();
        }
        else
        {
            cube = CreateObject(_prefab);
        }

        return cube != null;
    }

    private Cube GetObject()
    {
        Cube takenObject = _pool.Dequeue();
        takenObject.gameObject.SetActive(true);

        return takenObject;
    }

    public void ReturnObject(Cube returnedObject)
    {
        returnedObject.gameObject.SetActive(false);
        _pool.Enqueue(returnedObject);
    }

    private Cube CreateObject(Cube prefab)
    {
        return Instantiate(prefab);
    }
}