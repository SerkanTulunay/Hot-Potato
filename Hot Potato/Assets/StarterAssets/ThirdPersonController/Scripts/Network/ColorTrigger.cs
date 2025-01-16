using Unity.Netcode;
using UnityEngine;

public class ColorTrigger : NetworkBehaviour
{
    public NetworkVariable<Color> networkColor = new NetworkVariable<Color>(Color.white);
    [SerializeField] ServerGameManager gameManager;

    private bool IsTaken;
    private Material material;

    public override void OnNetworkSpawn()
    {
        networkColor.OnValueChanged += OnColorChanged;
        MeshRenderer meshRend = GetComponent<MeshRenderer>();
        if(meshRend != null )
        {
            material = new Material(meshRend.material);
            meshRend.material = material;
            UpdateMaterialColor(networkColor.Value);
        }
        base.OnNetworkSpawn();
    }

    public override void OnNetworkDespawn()
    {
        networkColor.OnValueChanged -= OnColorChanged;
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        UpdateMaterialColor(newColor);
    }

    private void UpdateMaterialColor(Color newColor)
    {
        if(material != null)
        {
            material.SetColor("_BaseColor", newColor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NetworkObject networkObject = other.GetComponent<NetworkObject>();
        if (IsClient && networkObject != null && networkObject.IsOwner)
        {
            ChangePositionServerRpc(networkObject.OwnerClientId);
        }
    }

    [Rpc(SendTo.Server)]
    private void ChangePositionServerRpc(ulong playerId)
    {
        if (IsTaken)
            return;

        if(playerId % 2 == 0)
        {
            ServerGameManager.instance.GiveBluePoint();
        }
        else
        {
            ServerGameManager.instance.GiveRedPoint();
        }

        ServerGameManager.instance.MoveCube();
    }
}
