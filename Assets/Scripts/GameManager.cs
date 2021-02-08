using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour
{
    public bool helpMode = true;
    public TMPro.TextMeshProUGUI mistakeCounter;

    private Tile[] _tiles;
    

    private void Start()
    {
        _tiles = FindObjectsOfType<Tile>();
    }

    
    public bool CheckIfSolved()
    {
        // Checks each tile to see if its solved or not
        foreach (Tile tile in _tiles)
        {
            if (tile.solved != tile.State)
            {
                return false;
            }
        }

        return true;
    }


    public void AddMistake()
    {
        // In helpMode, the game lets the player know they got a tile wrong and adds 1 to a mistake counter
        int oldNum = System.Convert.ToInt32(Regex.Match(mistakeCounter.text, @"\d+").Value);
        string newNum = (oldNum + 1).ToString();
        mistakeCounter.text = Regex.Replace(mistakeCounter.text, @"\d+", newNum);
    }
}
