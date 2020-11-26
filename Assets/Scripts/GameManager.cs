using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Tile[] _tiles;

    private void Start()
    {
        
        _tiles = FindObjectsOfType<Tile>();
    }


    //Checks each tile to see if its solved or not
    public void CheckIfSolved()
    {
        foreach (Tile tile in _tiles)
        {
            bool solved = tile.solved == tile.state;
        }
    }
}
