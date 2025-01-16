using UnityEngine;
using Unity.Netcode;
public class ServerSpawnCube : NetworkBehaviour
{
    [SerializeField] GameObject cube;
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            return;


        SpawnCube();
        base.OnNetworkSpawn();
    }

    void SpawnCube()
    {
        var spawnPoint = ServerPlayerSpawnPoints.Instance.GetRandomSpawnPoint();
        var SpawnPosition = spawnPoint ? spawnPoint.transform.position : Vector3.zero;
        cube.transform.position = SpawnPosition;
    }
}
