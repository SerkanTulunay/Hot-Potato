using UnityEngine;
using Unity.Netcode;
using StarterAssets;
using System.Net.Sockets;
public class ServerPotato : NetworkBehaviour
{
    NetworkVariable<ulong> ownerID = new NetworkVariable<ulong>();
    ThirdPersonController tpc;
    NetworkVariable<bool> isThrown = new NetworkVariable<bool>();
    [SerializeField] Rigidbody rb;

    public static ServerPotato instance;


    public void Awake()
    {
        if(instance != null && instance != this)
        {
            return;
        }
        else
        {
            instance = this;
        }
        ownerID.Value = default;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }

    private void Update()
    {
        if(tpc && !isThrown.Value)
        {
            gameObject.transform.position = tpc.ballHolderTransform.position;
            rb.useGravity = false;
        }

        if(isThrown.Value == true)
            rb.useGravity = true;
        
    }

    public void ThrowPotato(Vector3 forward,ulong playerID)
    {
        ThrowPotatoServerRpc(forward.x, forward.y, forward.z, playerID);
    }

    [Rpc(SendTo.Server)]
    private void ThrowPotatoServerRpc(float x, float y, float z, ulong playerID)
    {
        if (playerID != ownerID.Value)
            return;

        if (isThrown.Value)
            return;

        isThrown.Value = true;
        ownerID.Value = default;

        Vector3 dir = new Vector3(x, y, z);
        rb.AddForce(dir.normalized * 10, ForceMode.Impulse);
        Debug.Log("THROWING ON SERVER " + x + " " + y + " " + z);
    }

    private void OnTriggerEnter(Collider other)
    {
        NetworkObject networkObject = other.GetComponent<NetworkObject>();
        Debug.Log("COLLIDED");
        if (IsClient && networkObject != null)
        {
            isThrown.Value = false;
            tpc = other.GetComponent<ThirdPersonController>();
            rb.angularVelocity = Vector3.zero;
            rb.linearVelocity = Vector3.zero;
            UpdateBallOwnerServerRpc(networkObject.OwnerClientId);
        }
    }

    [Rpc(SendTo.Server)]
    private void UpdateBallOwnerServerRpc(ulong newOwner)
    {
        ownerID.Value = newOwner;
    }
}
