using UnityEngine;
using Unity.Netcode;
using StarterAssets;
public class ServerPotato : NetworkBehaviour
{
    NetworkVariable<int> ownerID = new NetworkVariable<int>();

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
    }

    public override void OnNetworkSpawn()
    {
        if(!IsOwner)
        {
            enabled = false; 
            return;
        }

        base.OnNetworkSpawn();
    }

    public void ThrowPotato(Vector3 forward)
    {
        ThrowPotatoServerRpc(forward.x, forward.y, forward.z);
    }

    [Rpc(SendTo.Server)]
    private void ThrowPotatoServerRpc(float x, float y, float z)
    {
        gameObject.transform.position += new Vector3(x, y, z);
        Debug.Log("THROWING ON SERVER " + x + " " + y + " " + z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
