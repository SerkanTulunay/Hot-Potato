using TMPro;
using UnityEngine;

public class ChatMessage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;

    public void SetText(string text)
    {
        messageText.text = text;
    }
}
