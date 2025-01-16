using Unity.Netcode;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class ServerPlayerMove : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if(!IsServer)
        {
            enabled = false;
            return;
        }
        
        SpawnPlayer();
        base.OnNetworkSpawn();
    }

    void SpawnPlayer()
    {
        var spawnPoint = ServerPlayerSpawnPoints.Instance.GetRandomSpawnPoint();
        var spawnPosition = spawnPoint ? spawnPoint.transform.position : Vector3.zero;
        transform.position = spawnPosition;
    }
}
