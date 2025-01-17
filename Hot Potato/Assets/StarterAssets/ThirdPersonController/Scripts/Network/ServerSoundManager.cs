using Unity.Netcode;
using UnityEngine;

public class ServerSoundManager : NetworkBehaviour
{
    public static ServerSoundManager instance;
    [SerializeField] AudioClip tauntClip;
    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsClient)
        {
            enabled = false;
            return;
        }
            
        base.OnNetworkSpawn();
    }

    public void PlayerTauntSound()
    {
        audioSource.clip = tauntClip;
        PlayTauntSoundServerRpc();
    }

    [Rpc(SendTo.Server)]
    private void PlayTauntSoundServerRpc()
    {
        PlayTauntSoundClientRpc();
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void PlayTauntSoundClientRpc()
    {
        audioSource.Play();
        Debug.Log("THROUG SERVER");
    }
}
