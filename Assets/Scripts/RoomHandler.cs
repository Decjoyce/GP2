using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RoomHandler : MonoBehaviour
{
    public List<string> rooms;
    public Transform[] roomCamPositions;

    [SerializeField] Camera player1Cam, player2Cam, mainCam;
    [SerializeField] GameObject sharedCanvas, splitCanvas;
    [SerializeField] PlayerCamera p1Cam, p2Cam, sharedCam;
    [SerializeField] CinemachineTargetGroup tg;
    public string roomPlayer1, roomPlayer2;
    bool playerDead;

    private void Start()
    {
        ChangeRoom("Room0", true);
        ChangeRoom("Room0", false);
    }

    public void ChangeRoom(string roomName, bool player2)
    {
        if (!playerDead)
        {
            if (!player2)
            {
                roomPlayer1 = roomName;
                int i = rooms.IndexOf(roomName);
                p1Cam.ChangeCameraPos(roomCamPositions[i].position);
            }
            else
            {
                roomPlayer2 = roomName;
                int i = rooms.IndexOf(roomName);
                p2Cam.ChangeCameraPos(roomCamPositions[i].position);
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
            sharedCam.ChangeCameraPos(roomCamPositions[i].position);
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
            sharedCam.ChangeCameraPos(roomCamPositions[i].position);
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
        sharedCam.ChangeCameraPos(roomCamPositions[i].position);
    }
}
