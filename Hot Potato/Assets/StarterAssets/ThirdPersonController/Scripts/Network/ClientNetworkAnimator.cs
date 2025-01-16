using UnityEngine;
using Unity.Netcode.Components;
using Unity.Netcode;

[DisallowMultipleComponent]
public class ClientNetworkAnimator : NetworkAnimator
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
