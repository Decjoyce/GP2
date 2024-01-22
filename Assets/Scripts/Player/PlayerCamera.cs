using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera currentCam;
    [SerializeField] Transform playerLookAt;

    public void ChangeCameraPos(Vector3 camPosition)
    {
        currentCam.transform.position = camPosition;
    }
}
