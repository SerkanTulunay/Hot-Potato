using UnityEngine;
using Unity.Netcode;
public class ServerGameManager : NetworkBehaviour
{
    public NetworkVariable<int> redScore = new NetworkVariable<int>();
    public NetworkVariable<int> blueScore = new NetworkVariable<int>();
    public NetworkVariable<float> timer = new NetworkVariable<float>();

    [SerializeField] GameObject cube;
    public static ServerGameManager instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            enabled = false;
            return;
        }

        redScore.Value = 0;
        blueScore.Value = 0;
        base.OnNetworkSpawn();
    }

    private void Update()
    {
        timer.Value += Time.deltaTime;
    }

    public void GiveRedPoint()
    {
        redScore.Value++;
    }

    public void GiveBluePoint()
    {
        blueScore.Value++;
    }

    public void MoveCube()
    {
        var spawnPoint = ServerPlayerSpawnPoints.Instance.GetRandomSpawnPoint();
        var spawnPosition = spawnPoint ? spawnPoint.transform.position : Vector3.zero;
        cube.transform.position = spawnPosition;
    }
}
