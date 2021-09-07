 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject northWall, northPass, eastWall, eastPass, southWall, southPass, westWall, westPass; //, rockPrefab;
    //public GameObject[] treePrefab, groundPrefab;
    //public int[,] groundMap, treeMap;

    //private int sparsity = 50;

    private void Awake()
    {
        //groundMap = new int[15, 15]; for (int _y = 0; _y < 15; _y++) for (int _x = 0; _x < 15; _x++) groundMap[_x, _y] = -1; 
        //treeMap = new int[15, 15]; for (int _y = 0; _y < 15; _y++) for (int _x = 0; _x < 15; _x++) treeMap[_x, _y] = -1;
    }

    private void Start()
    {
        //ShuffleTiles();
        //DrawTiles();
    }

    /*
    void ShuffleTiles()
    {
        int _random = 0;
        GameObject[] _ground = GameObject.FindGameObjectsWithTag("Ground");
        GameObject[] _tree = GameObject.FindGameObjectsWithTag("Tree");
        foreach(GameObject _tile in _ground)
        {
            _random = Random.Range(0, groundPrefab.Length + sparsity + 1);
            if (_random > groundPrefab.Length - 1) _random = 0;
            _tile.GetComponentInChildren<SpriteRenderer>().sprite = groundPrefab[_random].GetComponentInChildren<SpriteRenderer>().sprite;
        }

        foreach(GameObject _tile in _tree)
        {
            if (Random.Range(0, sparsity) < (sparsity * 0.3f)) 
                _random = Random.Range(0, treePrefab.Length);
            else
                _random = Random.Range(0, 5);
            Instantiate(treePrefab[_random], _tile.transform.position, Quaternion.identity, _tile.transform.parent);
            Destroy(_tile);
        }
    }

    void DrawTiles()
    {

    }
    */
}