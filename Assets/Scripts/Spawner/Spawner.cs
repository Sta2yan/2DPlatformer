using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _item;

    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    private void OnEnable()
    {
        Spawn();
    }

    public void AddSpawnPoint(SpawnPoint spawnPoint)
    {
        _spawnPoints.Add(spawnPoint);
    }

    private void Spawn()
    {
        foreach (var point in _spawnPoints)
            Instantiate(_item, point.transform);
    }
}
