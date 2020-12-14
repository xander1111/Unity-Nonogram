using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Tile tile;
    public Num num;
    public int boardWidth;
    public int boardHeight;

    private float _sqRelScale = 0.0f;
    private float _sqScale = 1.0f;
    [SerializeField]
    private Tile[] _allTiles;


    void Awake()
    {
        CreateBoard();

        _allTiles = FindObjectsOfType<Tile>();
        GeneratePuzzle();

        GenerateUI();
    }


    private void CreateRow(int tileCount, float rowHorizOffset, float rowVertOffset, float squareScale)
    {
        // Creates a single row of tiles
        for (int i = 0; i < tileCount; i++)
        {
            float horizPos = (i - rowHorizOffset) * squareScale;
            float vertPos = rowVertOffset * squareScale;

            Vector2 tilePosition = new Vector2(horizPos, vertPos);

            Instantiate(tile, tilePosition, transform.rotation, this.transform);
        }
    }

    
    private void CreateBoard()
    {
        // Determines whether to scale the board based on vertical or horizontal size
        // Note: this assumes the display is a sqaure
        if (boardWidth > boardHeight)
        {
            _sqScale = 25 / boardWidth;

        }
        else
        {
            _sqScale = 25 / boardHeight;
        }
        tile.transform.localScale = new Vector2(_sqScale, _sqScale);


        // sqRelScale gotten from (tile sprite size * scale)
        // Represents the unit size of a tile
        _sqRelScale = (0.2f * tile.transform.localScale.x);

        for (float i = 0; i < boardHeight; i++)
        {
            // Finds how much to offset the entire board by as it would normally generate the bottom left tile in the middle of the screen
            float boardVertOffset = (boardHeight - 1) / 2.0f;
            float boardHorizOffset = (boardWidth - 1) / 2.0f;

            // Creates each row
            CreateRow(boardWidth, boardHorizOffset, i - boardVertOffset, _sqRelScale);
        }
    }


    private void GeneratePuzzle()
    {
        //Generates a random value for solved variable on every tile
        foreach (Tile tile in _allTiles)
        {
            int solved = Random.Range(0, 2);
            if (solved == 0)
            {
                tile.solved = -1;
            }
            else
            {
                tile.solved = 1;
            }
        }
    }
    

    private void GenerateUI()
    {
        // Generates the display numbers used for solving the puzzle

        float cornerTileVertDistance = (boardHeight - 1) / 2.0f * _sqRelScale;
        float cornerTileHorizDistance = (boardWidth - 1) / 2.0f * _sqRelScale;
        Vector2 lowerCorner = new Vector2(-cornerTileHorizDistance, -cornerTileVertDistance);
        Vector2 upperCorner = new Vector2(cornerTileHorizDistance, cornerTileVertDistance);

        // Columns
        for (int i = 0; i < boardWidth; i++)
        {
            Vector2 numColPosition = new Vector2(lowerCorner.x + (i * _sqRelScale), upperCorner.y + _sqRelScale);
            int numCount = 0;

            foreach (int solutionNum in FindColumnSolution(lowerCorner.x + (i * _sqRelScale)))
            {
                Vector2 offsetPos = new Vector2(numColPosition.x, numColPosition.y + (numCount * _sqRelScale));

                Num newNum = Instantiate(num, offsetPos, transform.rotation, this.transform);

                newNum.GetComponent<TMPro.TextMeshPro>().text = solutionNum.ToString();
                newNum.transform.localScale = new Vector3(_sqRelScale, _sqRelScale, 1);

                numCount++;
            }
        }

        // Rows
        for (int i = 0; i < boardHeight; i++)
        {
            Vector2 numRowPosition = new Vector2(lowerCorner.x - _sqRelScale, lowerCorner.y + (i * _sqRelScale));
            int numCount = 0;
            
            foreach (int solutionNum in FindRowSolution(lowerCorner.y + (i * _sqRelScale)))
            {
                Vector2 offsetPos = new Vector2(numRowPosition.x - (numCount * _sqRelScale), numRowPosition.y);

                Num newNum = Instantiate(num, offsetPos, transform.rotation, this.transform);

                newNum.GetComponent<TMPro.TextMeshPro>().text = solutionNum.ToString();
                newNum.transform.localScale = new Vector3(_sqRelScale, _sqRelScale, 1);

                numCount++;
            }
        }
    }


    private List<int> FindRowSolution(float rowY)
    {
        // Returns the UI shown solution for a specified row

        List<Tile> rowTiles = new List<Tile>();
   
        // Creates a list of all tiles in the specified row
        foreach (Tile tile in _allTiles)
        {
            if (Mathf.Approximately(tile.transform.position.y, rowY))
            {
                rowTiles.Add(tile);
            }
        }
        // Sorts tiles from right to left, same direction the numbers are created in
        rowTiles.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x));
        rowTiles.Reverse();

        return FindListSolution(rowTiles);
    }


    private List<int> FindColumnSolution(float colX)
    {
        // Returns the UI shown solution for a specified column

        List<Tile> colTiles = new List<Tile>();

        // Creates a list of all tiles in the specified column
        foreach (Tile tile in _allTiles)
        {
            if (Mathf.Approximately(tile.transform.position.x, colX))
            {
                colTiles.Add(tile);
            }
        }
        // Sorts tiles from bottom to top, same direction the numbers are created in
        colTiles.Sort((a, b) => a.transform.position.y.CompareTo(b.transform.position.y));

        return FindListSolution(colTiles);
    }


    private List<int> FindListSolution(List<Tile> tiles)
    {
        List<int> solution = new List<int>();
        int concurrentTiles = 0;

        foreach (Tile tile in tiles)
        {
            if (tile.solved == 1)
            {
                concurrentTiles++;
            }
            else
            {
                if (concurrentTiles != 0)
                {
                    solution.Add(concurrentTiles);
                }
                concurrentTiles = 0;
            }
        }
        if (concurrentTiles != 0)
        {
            solution.Add(concurrentTiles);
        }

        if (solution.Count == 0)
        {
            solution.Add(0);
        }


        return solution;
    }
}
