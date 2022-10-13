using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] public float SpawnRate;
    [SerializeField]

    private float _spawnTime;
    private Transform _transform;
    private Stack<Transform> _pool;
    private Queue<Transform> _spawned;

    public Transform SpawnPoint => _spawnPoint;
    public Transform[] Spawned => _spawned.ToArray();

    private void Awake()
    {
        _pool = new Stack<Transform>();
        _spawned = new Queue<Transform>();
        _transform = transform;
    }

    private void Update()
    {
        _spawnTime += Time.deltaTime;

        if (_spawnTime >= SpawnRate)
        {
            Spawn();
            _spawnTime = 0;
        }
    }

    public Transform Spawn()
    {
        Transform obj;
        if (_pool.Count == 0)
        {
            obj = Instantiate(_prefab, _transform);
            _spawned.Enqueue(obj.transform);
        }
        else
        {
            obj = _pool.Pop();
            _spawned.Enqueue(obj);
        }

        obj.position = _spawnPoint.position;
        obj.gameObject.SetActive(true);

        return obj;
    }

    public Transform DespawnLast()
    {
        var obj = _spawned.Dequeue();
        _pool.Push(obj);
        obj.gameObject.SetActive(false);
        return obj;
    }
}
