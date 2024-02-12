using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class LayoutDecider : MonoBehaviour
{
    [SerializeField] List<string> rooms = new List<string>();
    [SerializeField] int gridLength = 7;
    Dictionary<Vector2, int> grid = new Dictionary<Vector2, int>();

    Vector2 player1Spawn, player2Spawn;

    // Start is called before the first frame update
    void Start()
    {
        ChooseSpawnRooms();
        SetUpCampfire();
        for (int y = 0; y < gridLength; y++)
        {
            Debug.Log("-------------- = " + y);
            for (int x = 0; x < gridLength; x++)
            {

                CheckRooms(x, y);

            }
        }
        string s = "Tom";
        s.Replace("T", "M");
        Debug.Log(s);
    }

    void ChooseSpawnRooms()
    {
        for (int i = 0; i < 2; i++)
        {
            string newRoom = "0000";
            int axis = Random.Range(0, 4); // 0 = first x axis, 1 = last x axis, 2 = first y axis, 3 = last y axis
            int x = -1;
            int y = -1;
            switch (axis)
            {
                case 0:
                    x = 0;
                    y = Random.Range(1, gridLength - 1);
                    while (y == player1Spawn.y)
                    {
                        y = Random.Range(1, gridLength - 1);
                    }

                    newRoom = newRoom.ReplaceAt(1, 1.ToString()[0]);
                    break;
                case 1:
                    x = gridLength - 1;
                    y = Random.Range(1, gridLength - 1);
                    while (y == player1Spawn.y)
                    {
                        y = Random.Range(1, gridLength - 1);
                    }

                    newRoom = newRoom.ReplaceAt(3, 1.ToString()[0]);
                    break;
                case 2:
                    y = 0;
                    x = Random.Range(1, gridLength - 1);
                    while (x == player1Spawn.x)
                    {
                        x = Random.Range(1, gridLength - 1);
                    }

                    newRoom = newRoom.ReplaceAt(0, 1.ToString()[0]);
                    break;
                case 3:
                    y = gridLength - 1;
                    x = Random.Range(1, gridLength - 1);
                    while (x == player1Spawn.x)
                    {
                        x = Random.Range(1, gridLength - 1);
                    }

                    newRoom = newRoom.ReplaceAt(2, 1.ToString()[0]);
                    break;
            }
            if (i == 0)
                player1Spawn = new Vector2(x, y);
            else
                player2Spawn = new Vector2(x, y);
            grid[new Vector2(x, y)] = rooms.FindIndex(a => a == newRoom);
        }
    }

    void SetUpCampfire()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
            grid[new Vector2(3, 3)] = rooms.FindIndex(a => a == "1000");
                break;
            case 1:
                grid[new Vector2(3, 3)] = rooms.FindIndex(a => a == "0100");
                break;
            case 2:
                grid[new Vector2(3, 3)] = rooms.FindIndex(a => a == "0010");
                break;
            case 3:
                grid[new Vector2(3, 3)] = rooms.FindIndex(a => a == "0001");
                break;
        }
    }

    void CheckRooms(int x, int y)
    {
        string newRoom = "0000";

        string roomBottom = null;
        string roomLeft = null;
        if (y == 0 || y == gridLength - 1 || x == 0 || x == gridLength - 1)
        {
            if ((x == player1Spawn.x && y == player1Spawn.y) || (x == player2Spawn.x && y == player2Spawn.y))
            {
                Debug.Log(newRoom + " " + x + " " + y + " " + grid[new Vector2(x, y)] + " SpawnPoint");
                return;
            }
            //BeachVariants
            grid[new Vector2(x, y)] = rooms.FindIndex(a => a == newRoom);
            Debug.Log(newRoom + " " + x + " " + y + " " + grid[new Vector2(x, y)]);
            return;
        }

        if(x == 3 && y == 3)
        {
            Debug.Log(rooms[grid[new Vector2(3, 3)]] + " " + x + " " + y + " " + grid[new Vector2(3, 3)] + " CAMPFIRE!!!!!!!!!!!");
            return;
        }
        //Check the nearby rooms
        if (y > 0)
        {
            roomBottom = rooms[grid[new Vector2(x, y - 1)]];
            if (int.Parse(roomBottom[0].ToString()) == 1)
            {
                newRoom = newRoom.ReplaceAt(2, 1.ToString()[0]);
            }
        }

        if (x > 0)
        {
            roomLeft = rooms[grid[new Vector2(x - 1, y)]];

            if (int.Parse(roomLeft[1].ToString()) == 1)
            {
                newRoom = newRoom.ReplaceAt(3, 1.ToString()[0]);
            }
        }

        if ((x == player1Spawn.x - 1 && y == player1Spawn.y) || (x == player2Spawn.x - 1 && y == player2Spawn.y))
        {
            newRoom = newRoom.ReplaceAt(1, 1.ToString()[0]);
        }

        if((x == player1Spawn.x && y == player1Spawn.y - 1) || (x == player2Spawn.x && y == player2Spawn.y - 1))
        {
            newRoom = newRoom.ReplaceAt(0, 1.ToString()[0]);
        }

        int entrances = newRoom.AmountOf(1.ToString()[0]);

        if(y != gridLength - 2 && entrances > 1)
        {
            newRoom = newRoom.ReplaceAt(0, Random.Range(0, 2).ToString()[0]);
        }
        else if(y != gridLength - 2)
        {
            newRoom = newRoom.ReplaceAt(0, 1.ToString()[0]);
        }

        if (x != gridLength - 2 && entrances > 1)
        {
            newRoom = newRoom.ReplaceAt(1, Random.Range(0, 2).ToString()[0]);
        }
        else if (x != gridLength - 2)
        {
            newRoom = newRoom.ReplaceAt(1, 1.ToString()[0]);
        }

        if (y == 3)
        {
            if(x == 2)
            {
                if (rooms[grid[new Vector2(3, 3)]] == "0001")
                    newRoom = newRoom.ReplaceAt(1, 1.ToString()[0]);
                else
                    newRoom = newRoom.ReplaceAt(1, 0.ToString()[0]);
            }
        }
        if (x == 3)
        {
            if (y == 2)
            {
                if (rooms[grid[new Vector2(3, 3)]] == "0010")
                    newRoom = newRoom.ReplaceAt(0, 1.ToString()[0]);
                else
                    newRoom = newRoom.ReplaceAt(0, 0.ToString()[0]);
            }
        }

        //MoreLogic
        grid[new Vector2(x, y)] = rooms.FindIndex(a => a == newRoom);
        Debug.Log(newRoom + " " + x + " " + y + " " + grid[new Vector2(x, y)]);
    }

}
