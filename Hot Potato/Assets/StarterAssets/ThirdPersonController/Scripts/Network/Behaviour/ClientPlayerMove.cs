using UnityEngine;
using Unity.Netcode;
using StarterAssets;
using UnityEngine.InputSystem;
using Cinemachine;
public class ClientPlayerMove : NetworkBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] ThirdPersonController thirdPersonController;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        playerInput.enabled = false;
        thirdPersonController.enabled = false;
        characterController.enabled = false;
        virtualCamera.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        enabled = IsClient;
        if(!IsOwner)
        {
            enabled = false;
            playerInput.enabled = false;
            thirdPersonController.enabled = false;
            characterController.enabled = false;
            virtualCamera.enabled= false;
            return;
        }

        playerInput.enabled = true;
        thirdPersonController.enabled = true;
        characterController.enabled = true;
        virtualCamera.enabled = true;
    }
}
