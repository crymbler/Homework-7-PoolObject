using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _minLifetime;
    [SerializeField] private int _maxLifetime;

    private bool _isChanged;
    private ISpawnerReceiver _spawnerReceiver;

    public void Initialize(ISpawnerReceiver spawnerReceiver)
    {
        _spawnerReceiver = spawnerReceiver;
        _isChanged = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            ChangeColor();

            StartCoroutine(ToStartLifetime(Random.Range(_minLifetime, _maxLifetime)));
        }
    }

    private void ChangeColor()
    {
        if (!_isChanged)
        {
            GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);

            _isChanged = true;
        }
    }

    private IEnumerator ToStartLifetime(int time)
    {
        yield return new WaitForSeconds(time);

        _spawnerReceiver?.OnLifetimeEnded(this);
    }
}