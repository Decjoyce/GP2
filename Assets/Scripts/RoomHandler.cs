using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{

    #region singleton
    public static RoomHandler instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of RoomHandler found");
            return;
        }
        instance = this;
    }
    #endregion singleton

    public List<string> rooms;
    public Transform[] roomCamPositions;

    [SerializeField] Camera player1Cam, player2Cam, mainCam;
    [SerializeField] CinemachineVirtualCamera player1VirtualCam, player2VirtualCam, sharedVirtualCam;
    [SerializeField] GameObject sharedCanvas, splitCanvas;
    [SerializeField] CinemachineTargetGroup tg;
    public string roomPlayer1, roomPlayer2;
    bool playerDead;

    private void Start()
    {
        if (roomPlayer2 == "" || roomPlayer2 == null)
        {
            roomPlayer2 = rooms[0];
        }
        if (roomPlayer1 == "" || roomPlayer1 == null)
        {
            roomPlayer1 = rooms[0];
        }
        Debug.Log(roomPlayer1 + roomPlayer2);
    }

    public void SetUpRooms(string nameOfRoom, bool player2)
    {
        string roomName = nameOfRoom;
        if (roomName == "" || roomName == null)
        {
            roomName = rooms[0];
        }

        if (!playerDead)
        {
            if (!player2)
            {
                roomPlayer1 = roomName;
                int i = rooms.IndexOf(roomName);
                Debug.Log(i);
                player1VirtualCam.transform.position = roomCamPositions[i].position;
            }
            else
            {
                roomPlayer2 = roomName;
                int i = rooms.IndexOf(roomName);
                player2VirtualCam.transform.position = roomCamPositions[i].position;
            }
            if (roomPlayer1 == roomPlayer2)
            {
                SetSplitScreen(false);
            }
            else
                SetSplitScreen(true);
        }
        else
        {
            int i = 0;
            if (!player2)
            {
                roomPlayer1 = roomName;
                i = rooms.IndexOf(roomName);
            }
            else
            {
                roomPlayer2 = roomName;
                i = rooms.IndexOf(roomName);
            }
            sharedVirtualCam.transform.position = roomCamPositions[i].position;
        }
    }

    public void ChangeRoom(string roomName, bool player2)
    {
        if (!playerDead)
        {
            if (!player2)
            {
                roomPlayer1 = roomName;
                int i = rooms.IndexOf(roomName);
                Debug.Log(i);
                player1VirtualCam.transform.position = roomCamPositions[i].position;
            }
            else
            {
                roomPlayer2 = roomName;
                int i = rooms.IndexOf(roomName);
                player2VirtualCam.transform.position = roomCamPositions[i].position;
            }
            if (roomPlayer1 == roomPlayer2)
            {
                SetSplitScreen(false);
            }
            else
                SetSplitScreen(true);
        }
        else
        {
            int i = 0;
            if (!player2)
            {
                roomPlayer1 = roomName;
                i = rooms.IndexOf(roomName);
            }
            else
            {
                roomPlayer2 = roomName;
                i = rooms.IndexOf(roomName);
            }
            sharedVirtualCam.transform.position = roomCamPositions[i].position;
        }
    }

    void SetSplitScreen(bool set)
    {
        if (!set)
        {
            mainCam.enabled = true;
            player1Cam.enabled = false;
            player2Cam.enabled = false;
            sharedCanvas.SetActive(true);
            splitCanvas.SetActive(false);
            int i = rooms.IndexOf(roomPlayer1);
            sharedVirtualCam.transform.position = roomCamPositions[i].position;
        }
        else
        {
            mainCam.enabled = false;
            player1Cam.enabled = true;
            player2Cam.enabled = true;
            sharedCanvas.SetActive(false);
            splitCanvas.SetActive(true);
        }
    }

    public void SetUpDeath(bool player2)
    {
        playerDead = true;
        mainCam.enabled = true;
        player1Cam.enabled = false;
        player2Cam.enabled = false;
        splitCanvas.SetActive(false);
        sharedCanvas.SetActive(true);

        int i = 0;
        if (player2)
        {
            roomPlayer2 = "";
            i = rooms.IndexOf(roomPlayer1);
            tg.m_Targets[1].weight = 0;
        }
        else
        {
            roomPlayer1 = "";
            i = rooms.IndexOf(roomPlayer2);
            tg.m_Targets[0].weight = 0;
        }
        Debug.Log(i);
        sharedVirtualCam.transform.position = roomCamPositions[i].position;
    }
}
