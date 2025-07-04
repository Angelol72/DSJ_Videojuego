using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance { get; private set; }

    private List<RandomSpawnerArea> spawners = new List<RandomSpawnerArea>();
    private int activeEnemyCount = 0;
    public int ActiveEnemyCount => activeEnemyCount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterSpawner(RandomSpawnerArea spawner)
    {
        if (!spawners.Contains(spawner))
            spawners.Add(spawner);
    }

    public void UnregisterSpawner(RandomSpawnerArea spawner)
    {
        if (spawners.Contains(spawner))
            spawners.Remove(spawner);
    }

    public bool AllSpawnersAreFinished()
    {
        foreach (var spawner in spawners)
        {
            if (!spawner.isFinished)
            {
                return false;
            }
        }
        return true;
    }

    public void ResetAllSpawners()
    {
        foreach (var spawner in spawners)
        {
            spawner.ResetSpawner();
        }
    }

    public void ClearSpawners()
    {
        spawners.Clear();
    }

    public List<RandomSpawnerArea> GetAllSpawners()
    {
        return new List<RandomSpawnerArea>(spawners);
    }

    public void IncrementEnemyCount()
    {
        activeEnemyCount++;
    }

    public void DecrementEnemyCount()
    {
        activeEnemyCount = Mathf.Max(0, activeEnemyCount - 1);
    }

    public void ResetEnemyCount()
    {
        activeEnemyCount = 0;
    }
}
