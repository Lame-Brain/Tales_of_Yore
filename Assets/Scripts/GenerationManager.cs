using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    public static GenerationManager instance;

    public int mapWidth = 11;
    public int mapHeight = 11;
    public int roomsToGenerate = 12;
    public List<SpriteRenderer> treePrefab, groundPrefab;
    public GameObject[] loot1, loot2, loot3, loot4;

    private int roomIndex;
    private bool roomsInstantiated;

    private Vector2 firstRoomPos;
    private bool[,] map;
    public GameObject roomPrefab, chestPrefab, dwarfPrefab, rangerPrefab, gnomePrefab, gemPrefab, signPrefab;
    public List<Room> roomObjects = new List<Room>();

    void Awake()
    {
        instance = this;
    }

    public void Generate()
    {
        map = new bool[mapWidth, mapHeight];
        for (int y = 0; y < mapHeight; y++) for (int x = 0; x < mapWidth; x++) map[x, y] = false;
        roomsInstantiated = false;
        roomIndex = 0;
        roomObjects.Clear();
        CheckRoom(5, 5, 0, Vector2.zero, true);
        InstantiateRooms();
        FindObjectOfType<Player>().transform.position = firstRoomPos * 15;
    }

    void CheckRoom(int x, int y, int remaining, Vector2 generalDirection, bool firstRoom = false)
    {
        if (roomIndex >= roomsToGenerate) //Cancel if RoomIndex is higher or equal to the total number of rooms
            return;
        if (x < 0 || x > mapWidth - 1 || y < 0 || y > mapHeight - 1) //Cancel if room is beyond bounds set by map 2D-array (prevents overflow error)
            return;
        if (remaining <= 0 && !firstRoom) //Cancel if this branch has no more slots available (unless it is the first room)
            return;
        if (map[x, y]) //Cancel if this room already exists
            return;

        //OTHERWISE, Generate a room!
        if (firstRoom)// position origin room
        { 
            firstRoomPos = new Vector2(x, y);
            roomIndex = 0;
        }
        roomIndex++;
        map[x, y] = true;

        
        bool north = Random.value > (generalDirection == Vector2.up ? 0.2f : 0.8f);
        bool east = Random.value > (generalDirection == Vector2.right ? 0.2f : 0.8f);
        bool south = Random.value > (generalDirection == Vector2.down ? 0.2f : 0.8f);
        bool west = Random.value > (generalDirection == Vector2.left ? 0.2f : 0.8f);

        int maxRemaining = roomsToGenerate / 4;

        if (north || firstRoom)
            CheckRoom(x, y + 1, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.up : generalDirection);
        if (east || firstRoom)
            CheckRoom(x + 1, y, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.right : generalDirection);
        if (south || firstRoom)
            CheckRoom(x, y - 1, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.down : generalDirection);
        if (west || firstRoom)
            CheckRoom(x - 1, y, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.left : generalDirection);
        
    }

    void InstantiateRooms()
    {
        if (roomsInstantiated)
            return;        

        //INSTANTIATE ROOM PREFABS
        roomsInstantiated = true;
        for(int _y = 0; _y < mapHeight; _y++)
            for(int _x = 0; _x < mapWidth; _x++)
            {
                if (!map[_x, _y]) continue;

                Debug.Log("Made it past the filter");

                GameObject go = Instantiate(roomPrefab, new Vector3(_x, _y, 0) * 15, Quaternion.identity);
                Room room = go.GetComponent<Room>();

                if(_y < mapHeight - 1 && map[_x, _y + 1] == true)
                {
                    room.northWall.SetActive(false);
                    room.northPass.SetActive(true);
                }
                if (_x < mapWidth - 1 && map[_x + 1, _y] == true)
                {
                    room.eastWall.SetActive(false);
                    room.eastPass.SetActive(true);
                }
                if (_y > 0 && map[_x, _y - 1] == true)
                {
                    room.southWall.SetActive(false);
                    room.southPass.SetActive(true);
                }
                if (_x > 0 && map[_x - 1, _y] == true)
                {
                    room.westWall.SetActive(false);
                    room.westPass.SetActive(true);
                }
                roomObjects.Add(room);
                Debug.Log("added " + room.name);
            }

        //Randomize the Tiles
        int _random = 0;
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        foreach(GameObject _go in trees)
        {
            _random = Random.Range(0, treePrefab.Count);
            if (_go.GetComponent<SpriteRenderer>() == null) _go.AddComponent<SpriteRenderer>();
            _go.GetComponent<SpriteRenderer>().sprite = treePrefab[_random].sprite;            
        }

        GameObject[] ground = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject _go in ground)
        {
            _random = Random.Range(0, groundPrefab.Count+50);
            if (_random > groundPrefab.Count - 1) _random = 0;
            if(_go.GetComponent<SpriteRenderer>() == null) _go.AddComponent<SpriteRenderer>();
            _go.GetComponent<SpriteRenderer>().sprite = groundPrefab[_random].sprite;            
        }

        int _numChests = Random.Range(3, 9);
        for(int i = 0; i < _numChests; i++ ) PlaceObject("Chest");
        PlaceObject("Ranger");
        PlaceObject("Dwarf");
        PlaceObject("Gnome");
        PlaceObject("Gem");
        if (GameManager.GAME.ForestLevel < 18) PlaceObject("FSign");
        if (GameManager.GAME.ForestLevel > 1) PlaceObject("BSign");
    }

    void PlaceObject(string n)
    {
        Vector2 _pos, _basePos; RaycastHit2D hit; bool placed = false;
        while (!placed)
        {
            GameObject _go = null;
            _basePos = roomObjects[Random.Range(0, roomObjects.Count)].gameObject.transform.position;
            _pos = new Vector2(_basePos.x + Random.Range(-5, 5), _basePos.y + Random.Range(-5, 5));
            hit = Physics2D.Raycast(_pos, Vector2.zero, 0.5f);
            if (!hit)
            {
                if (n == "Chest") _go = Instantiate(chestPrefab, _pos, Quaternion.identity);
                if (n == "Ranger") _go = Instantiate(rangerPrefab, _pos, Quaternion.identity);
                if (n == "Dwarf") _go = Instantiate(dwarfPrefab, _pos, Quaternion.identity);
                if (n == "Gnome") _go = Instantiate(gnomePrefab, _pos, Quaternion.identity);
                if (n == "Gem") _go = Instantiate(gemPrefab, _pos, Quaternion.identity);
                if (n == "FSign") _go = Instantiate(signPrefab, _pos, Quaternion.identity);
                if (n == "BSign") _go = Instantiate(signPrefab, _pos, Quaternion.identity);
                placed = true;
            }
            
            if(n == "Chest")
            {
                int _r = Random.Range(1, 5); int _randomLoot = 0;
                for(int _i = 0; _i < _r; _i++)
                {                    
                    if (GameManager.GAME.ForestLevel < 5)
                    {
                        _randomLoot = Random.Range(-15, loot1.Length);
                        if (_randomLoot < 0) _randomLoot = 1;
                        if (_randomLoot < -5) _randomLoot = 0;
                        _go.GetComponent<Chest>().loot.Add(loot1[_randomLoot]);
                    }
                    if (GameManager.GAME.ForestLevel > 4 && GameManager.GAME.ForestLevel < 9)
                    {
                        _randomLoot = Random.Range(-15, loot2.Length);
                        if (_randomLoot < 0) _randomLoot = 1;
                        if (_randomLoot < -5) _randomLoot = 0;
                        _go.GetComponent<Chest>().loot.Add(loot2[_randomLoot]);
                    }
                    if (GameManager.GAME.ForestLevel > 8 && GameManager.GAME.ForestLevel < 13)
                    {
                        _randomLoot = Random.Range(-15, loot3.Length);
                        if (_randomLoot < 0) _randomLoot = 1;
                        if (_randomLoot < -5) _randomLoot = 0;
                        _go.GetComponent<Chest>().loot.Add(loot3[_randomLoot]);
                    }
                    if (GameManager.GAME.ForestLevel > 12)
                    {
                        _randomLoot = Random.Range(-15, loot4.Length);
                        if (_randomLoot < 0) _randomLoot = 1;
                        if (_randomLoot < -5) _randomLoot = 0;
                        _go.GetComponent<Chest>().loot.Add(loot4[_randomLoot]);
                    }
                }
            }

            if (n == "FSign") _go.GetComponent<Sign>().destination = GameManager.GAME.ForestLevel + 1;
            if (n == "BSign") _go.GetComponent<Sign>().destination = GameManager.GAME.ForestLevel - 1;
        }
    }
}

