using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    public int minRoomAmount = 5;
    public int maxRoomAmount = 10;
    private List<GameObject> rooms = new List<GameObject>();
    private int decidedRoomAmount;
    private GameObject roomsHolder;
    // Start is called before the first frame update
    void Start()
    {
        decidedRoomAmount = Random.Range(minRoomAmount, maxRoomAmount);
        GenerateDungeon();
        ConnectRooms();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log
    }

    private void GenerateDungeon()
    {
        roomsHolder = new GameObject("Dungeon");
        int nextRoomDirection;
        for (int r = 0; r < decidedRoomAmount; r++)
        {
            nextRoomDirection = Random.Range(1, 5);
            if (r == 0){rooms.Add(Instantiate(roomPrefab, new Vector3(0, 0, 0), Quaternion.identity));}
            else if (nextRoomDirection == 1 && rooms.Count >= 1) {
                if (!RoomIsHere(new Vector3(rooms[r - 1].transform.position.x + 5, 0, rooms[r - 1].transform.position.z)))
                {
                    rooms.Add(Instantiate(roomPrefab, new Vector3(rooms[r - 1].transform.position.x + 5, 0, rooms[r - 1].transform.position.z), Quaternion.identity));
                }
                else if (RoomIsHere(new Vector3(rooms[r - 1].transform.position.x + 5, 0, rooms[r - 1].transform.position.z)))
                {
                    r = r - 1;
                }
            }
            else if (nextRoomDirection == 2 && rooms.Count >= 1) {
                if (!RoomIsHere(new Vector3(rooms[r - 1].transform.position.x - 5, 0, rooms[r - 1].transform.position.z)))
                {
                    rooms.Add(Instantiate(roomPrefab, new Vector3(rooms[r - 1].transform.position.x - 5, 0, rooms[r - 1].transform.position.z), Quaternion.identity));
                }
                else if (RoomIsHere(new Vector3(rooms[r - 1].transform.position.x - 5, 0, rooms[r - 1].transform.position.z)))
                {
                    r = r - 1;
                }
            }
            else if (nextRoomDirection == 3 && rooms.Count >= 1) {
                if (!RoomIsHere(new Vector3(rooms[r - 1].transform.position.x, 0, rooms[r - 1].transform.position.z + 5)))
                {
                    rooms.Add(Instantiate(roomPrefab, new Vector3(rooms[r - 1].transform.position.x, 0, rooms[r - 1].transform.position.z + 5), Quaternion.identity));
                }
                else if (RoomIsHere(new Vector3(rooms[r - 1].transform.position.x, 0, rooms[r - 1].transform.position.z + 5)))
                {
                    r = r - 1;
                }
            }
            else if (nextRoomDirection == 4 && rooms.Count >= 1) {
                if (!RoomIsHere(new Vector3(rooms[r - 1].transform.position.x, 0, rooms[r - 1].transform.position.z - 5)))
                {
                    rooms.Add(Instantiate(roomPrefab, new Vector3(rooms[r - 1].transform.position.x, 0, rooms[r - 1].transform.position.z - 5), Quaternion.identity));
                }
                else if (RoomIsHere(new Vector3(rooms[r - 1].transform.position.x, 0, rooms[r - 1].transform.position.z - 5)))
                {
                    r = r - 1;
                }
            }
        }
        foreach(GameObject room in rooms)
        {
            room.transform.parent = roomsHolder.transform;
        }
    }
    private void ConnectRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (!RoomIsRight(rooms[i])) { rooms[i].transform.GetChild(2).gameObject.SetActive(false); rooms[i].transform.GetChild(3).gameObject.SetActive(true); }
            if (RoomIsRight(rooms[i])) { rooms[i].transform.GetChild(2).gameObject.SetActive(true); rooms[i].transform.GetChild(3).gameObject.SetActive(false); }
            if (!RoomIsLeft(rooms[i])) { rooms[i].transform.GetChild(0).gameObject.SetActive(false); rooms[i].transform.GetChild(1).gameObject.SetActive(true); }
            if (RoomIsLeft(rooms[i])) { rooms[i].transform.GetChild(0).gameObject.SetActive(true); rooms[i].transform.GetChild(1).gameObject.SetActive(false); }
            if (!RoomIsNorth(rooms[i])) { rooms[i].transform.GetChild(4).gameObject.SetActive(false); rooms[i].transform.GetChild(5).gameObject.SetActive(true); }
            if (RoomIsNorth(rooms[i])) { rooms[i].transform.GetChild(4).gameObject.SetActive(true); rooms[i].transform.GetChild(5).gameObject.SetActive(false); }
            if (!RoomIsSouth(rooms[i])) { rooms[i].transform.GetChild(6).gameObject.SetActive(false); rooms[i].transform.GetChild(7).gameObject.SetActive(true); }
            if (RoomIsSouth(rooms[i])) { rooms[i].transform.GetChild(6).gameObject.SetActive(true); rooms[i].transform.GetChild(7).gameObject.SetActive(false); }
        }
    }

    private bool RoomIsHere(Vector3 nextPosition)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].transform.position == nextPosition) { return true; }
        }
        return false;
    }

    private bool RoomIsLeft(GameObject room)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].transform.position == new Vector3(room.transform.position.x - 5, room.transform.position.y, room.transform.position.z)) { return true; }
        }
        return false;
    }
    private bool RoomIsRight(GameObject room)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].transform.position == new Vector3(room.transform.position.x + 5, room.transform.position.y, room.transform.position.z)) { return true; }
        }
        return false;
    }
    private bool RoomIsNorth(GameObject room)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].transform.position == new Vector3(room.transform.position.x, room.transform.position.y, room.transform.position.z + 5)) { return true; }
        }
        return false;
    }
    private bool RoomIsSouth(GameObject room)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].transform.position == new Vector3(room.transform.position.x, room.transform.position.y, room.transform.position.z - 5)) { return true; }
        }
        return false;
    }
}
