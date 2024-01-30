using System.Collections.Generic;
using UnityEngine;

public class LayoutDecider : MonoBehaviour
{
    [SerializeField] List<string> rooms = new List<string>();
    Dictionary<Vector2, int> grid = new Dictionary<Vector2, int>();

    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {

                CheckRooms(x, y);

            }
        }
        string s = "Tom";
        s.Replace("T", "M");
        Debug.Log(s);
    }

    void CheckRooms(int x, int y)
    {
        string newRoom = "0000";

        string roomBottom = null;
        string roomLeft = null;
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
        
        if(y != 4)
        {
            newRoom = newRoom.ReplaceAt(0, Random.Range(0, 2).ToString()[0]);
        }

        if (x != 4)
        {
            newRoom = newRoom.ReplaceAt(1, Random.Range(0, 2).ToString()[0]);
        }


        //MoreLogic
        //newRoom.Replace(newRoom[4], Random.Range(0, 6).ToString()[0]);
        grid[new Vector2(x, y)] = rooms.FindIndex(a => a == newRoom);
        Debug.Log(newRoom + " " + x + " " + y + " " + grid[new Vector2(x, y)]);
    }

}
