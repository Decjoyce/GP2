using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoom : MonoBehaviour
{
    [SerializeField] string roomName;
    [SerializeField] Transform cameraPos;
    [SerializeField] RoomHandler roomHandler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            roomHandler.ChangeRoom(roomName, false);
        }
        if (other.CompareTag("Player2"))
        {
            roomHandler.ChangeRoom(roomName, true);
        }
    }
}
