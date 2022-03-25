using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Spawner _spawner;

    private void OnEnable()
    {
        _spawner = GetComponentInParent<Spawner>();
        _spawner.AddSpawnPoint(this);
    }
}
