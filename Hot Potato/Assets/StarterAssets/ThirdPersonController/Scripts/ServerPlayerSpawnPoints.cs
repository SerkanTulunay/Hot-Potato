using System.Collections.Generic;
using UnityEngine;

public class ServerPlayerSpawnPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    private static ServerPlayerSpawnPoints instance;

    public static ServerPlayerSpawnPoints Instance {  get { return instance; } }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }


    public Transform GetRandomSpawnPoint()
    {
        if(spawnPoints.Count == 0)
        {
            return null;
        }

        return spawnPoints[Random.Range(0,spawnPoints.Count)];
    }
}
