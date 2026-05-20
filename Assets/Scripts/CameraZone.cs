using UnityEngine;
using Cinemachine;

public class CameraZone : MonoBehaviour
{
    public CinemachineVirtualCamera farCam;
    public CinemachineVirtualCamera closeCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            closeCam.Priority = 20;
            farCam.Priority = 10;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            closeCam.Priority = 10;
            farCam.Priority = 20;
        }
    }
}