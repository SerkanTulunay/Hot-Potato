using UnityEngine;
using Unity.Netcode;
using TMPro;
public class ChatManager : NetworkBehaviour
{
    public static ChatManager instance;

    [SerializeField] ChatMessage chatMsg;
    [SerializeField] CanvasGroup chatGroup;
    [SerializeField] TMP_InputField chatInput;


    public string playerName;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendChatMessage(chatInput.text, playerName);
            chatInput.text = "";
        }
    }

    public void SendChatMessage(string text, string playerName)
    {
        if (string.IsNullOrEmpty(text))
            return;

        string message = playerName + " : " + text;
        SendChatMessageServerRpc(message);
    }

    void AddMessage(string message)
    {
        ChatMessage CM = Instantiate(chatMsg, chatGroup.transform);
        CM.SetText(message);
    }

    [Rpc(SendTo.Server)]
    void SendChatMessageServerRpc(string message)
    {
        ReceiveChatMessageClientRpc(message);
    }

    [ClientRpc]
    void ReceiveChatMessageClientRpc(string message)
    {
        ChatManager.instance.AddMessage(message);
    }
}
