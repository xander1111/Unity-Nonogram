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


    // Checks each tile to see if its solved or not
    public bool CheckIfSolved()
    {
        bool solved = false;

        foreach (Tile tile in _tiles)
        {
            solved = tile.solved == tile.state;
        }

        return solved;
    }
}
