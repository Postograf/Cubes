using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Transform _endPoint;
    [SerializeField] public float Speed;

    public Transform EndPoint => _endPoint;

    private void Update()
    {
        var endPosition = _endPoint.position;
        foreach (var obj in _spawner.Spawned)
        {
            var direction = (endPosition - obj.position).normalized;
            var translation = direction * Speed * Time.deltaTime;

            if ((obj.position - endPosition).sqrMagnitude > translation.sqrMagnitude)
                obj.Translate(translation);
            else
                _spawner.DespawnLast();
        }
    }
}
