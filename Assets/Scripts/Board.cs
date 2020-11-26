using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Tile tile;
    public int boardWidth;
    public int boardHeight;

    private float _sqRelScale = 0.0f;
    private float _sqScale = 1.0f;


    void Awake()
    {
        // Determines whether to scale the board for vertical size or horizontal
        if (boardWidth > boardHeight)
        {
            _sqScale = 30 / boardWidth;
            
        }
        else
        {
            _sqScale = 30 / boardHeight;
        }
        tile.transform.localScale = new Vector2(_sqScale, _sqScale);


        // sqRelScale gotten from (tile sprite size * scale)
        _sqRelScale =  (0.2f * tile.transform.localScale.x);

        for (float i = 0; i < boardHeight; i++)
        {
            //Finds where the anchor square (bottom left) should be
            float boardVertOffset = (boardHeight - 1) / 2.0f;
            float boardHorizOffset = (boardWidth - 1) / 2.0f;

            //Makes each row
            CreateRow(boardWidth, boardHorizOffset, i - boardVertOffset);
        }


        GeneratePuzzle();
    }


    private void CreateRow(int tileCount, float rowHorizOffset, float rowVertOffset)
    {
        for (int i = 0; i < tileCount; i++)
        {
            float horizPos = (i - rowHorizOffset) * _sqRelScale;
            float vertPos = rowVertOffset * _sqRelScale;

            Vector2 tilePosition = new Vector2(horizPos, vertPos);

            Instantiate(tile, tilePosition, transform.rotation, this.transform);
        }
    }


    private List<Tile> GetAllTiles()
    {
        //Creates a list of all tiles
        List<Tile> tiles = new List<Tile>();
            
        for (int i = 0; i < this.transform.childCount; i++)
        {
            tiles.Add(this.transform.GetChild(i).GetComponent<Tile>());
        }

        return tiles;
    }

    
    private void GeneratePuzzle()
    {
        //Generates a random value for solved variable on every tile
        foreach (Tile tiles in GetAllTiles())
        {
            int solved = Random.Range(0, 2);
            if (solved == 0)
            {
                tiles.solved = -1;
            }
            else
            {
                tiles.solved = 1;
            }
        }
    }
}
