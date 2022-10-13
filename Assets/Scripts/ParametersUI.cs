using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class ParametersUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Mover _mover;

    [Header("Fields")]
    [SerializeField] private TMP_InputField _speedField;
    [SerializeField] private TMP_InputField _distanceField;
    [SerializeField] private float _maxDistance;
    [SerializeField] private TMP_InputField _spawnRateField;

    private void Start()
    {
        _speedField.text = _mover.Speed.ToString();
        var spawnPosition = _spawner.SpawnPoint.position;
        var endPosition = _mover.EndPoint.position;
        _distanceField.text = Vector3.Distance(spawnPosition, endPosition).ToString();
        _spawnRateField.text = _spawner.SpawnRate.ToString();

        _speedField.onSubmit.AddListener(ChangeSpeed);
        _distanceField.onSubmit.AddListener(ChangeDistance);
        _spawnRateField.onSubmit.AddListener(ChangeSpawnRate);
    }

    private void ChangeSpeed(string value)
    {
        _mover.Speed = Mathf.Max(0, float.Parse(value));
        _speedField.text = _mover.Speed.ToString();
    }

    private void ChangeDistance(string value)
    {
        var spawnPosition = _spawner.SpawnPoint.position;
        var endPosition = _mover.EndPoint.position;
        var direction = (endPosition - spawnPosition).normalized;
        var distance = Mathf.Clamp(float.Parse(value), 0, _maxDistance);
        _distanceField.text = distance.ToString();
        _mover.EndPoint.position = spawnPosition + direction * distance;
    }

    private void ChangeSpawnRate(string value)
    {
        _spawner.SpawnRate = Mathf.Max(0, float.Parse(value));
        _spawnRateField.text = _spawner.SpawnRate.ToString();
    }
}
