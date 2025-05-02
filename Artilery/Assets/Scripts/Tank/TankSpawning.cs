using UnityEngine;

public class TankSpawning : MonoBehaviour
{
    [Header("Prefabs & Spawn Points")]
    [SerializeField] private GameObject[] _tanksPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    [Header("Spawn Timing")]
    [SerializeField] private float _totalDuration = 1200f; 

    [SerializeField] private float _minSpawnInterval = 2f;

    [SerializeField] private float _maxSpawnInterval = 10f;

    private float _remainingTime;
    private float _nextSpawnTime;

    private void Start()
    {
        _remainingTime = _totalDuration;
        ScheduleNextSpawn();
    }

    private void Update()
    {
        _remainingTime = Mathf.Max(_remainingTime - Time.deltaTime, 0f);

        if (_remainingTime > 0f && Time.time >= _nextSpawnTime)
        {
            SpawnTank();
            ScheduleNextSpawn();
        }
    }

    private void SpawnTank()
    {
        var randomTank = Random.Range(0, _tanksPrefab.Length);
        var point = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        var tank = Instantiate(_tanksPrefab[randomTank], point.position, point.rotation);
    }

    private void ScheduleNextSpawn()
    {
        float progress = 1f - (_remainingTime / _totalDuration);
        float interval = Mathf.Lerp(_maxSpawnInterval, _minSpawnInterval, progress);
        _nextSpawnTime = Time.time + interval;
    }
}
