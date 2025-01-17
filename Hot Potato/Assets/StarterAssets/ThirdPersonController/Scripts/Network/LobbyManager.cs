using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] Button hostBtn, joinBtn;

    private void Awake()
    {
        AssignInputs();
    }

    void AssignInputs()
    {
        hostBtn.onClick.AddListener(delegate { NetworkManager.Singleton.StartHost(); });
        joinBtn.onClick.AddListener(delegate { NetworkManager.Singleton.StartClient(); });
    }
}
