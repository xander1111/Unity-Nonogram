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


    void Start()
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
    }


    private void CreateRow(int tileCount, float rowHorizOffset, float rowVertOffset)
    {
        for (int i = 0; i < tileCount; i++)
        {
            float horizPos = (i - rowHorizOffset) * _sqRelScale;
            float vertPos = rowVertOffset * _sqRelScale;

            Vector2 tilePosition = new Vector2(horizPos, vertPos);

            Instantiate(tile, tilePosition , transform.rotation, this.transform);
        }
    }
}
