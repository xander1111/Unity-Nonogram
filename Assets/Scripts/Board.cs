using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Tile tile;
    public Num num;
    public int boardWidth;
    public int boardHeight;

    private Tile[] _allTiles;
    // Store tiles in a 2d array so its easy to access specific tiles
    private Tile[,] _board;


    void Awake()
    {
        _board = new Tile[boardWidth, boardHeight];

        CreateBoard();

        _allTiles = FindObjectsOfType<Tile>();
        GeneratePuzzle();

        GenerateUI();
    }


    private void CreateRow(int tileCount, int rowVertOffset)
    {
        for (int i = 0; i < tileCount; i++)
        {
            Vector2 tilePosition = new Vector2(i, rowVertOffset);

            Tile newTile = Instantiate(tile, tilePosition, transform.rotation, this.transform);

            _board[i, rowVertOffset] = newTile;
        }

    }

    
    private void CreateBoard()
    {
        // Generates rows
        for (int i = 0; i < boardHeight; i++)
        {
            CreateRow(boardWidth, i);
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
        // Generate row hints
        for (int y = 0; y < boardHeight; y++)
        {
            List<int> rowSolution = FindRowSolution(y);

            for (int i = 0; i < rowSolution.Count; i++)
            {
                Vector2 numPosition = new Vector2(-1 - (i * 0.8f), y);

                Num newNum = Instantiate(num, numPosition, transform.rotation, this.transform);
                newNum.GetComponent<TMPro.TextMeshPro>().text = rowSolution[i].ToString();
            }
        }

        // Generate column hints
        for (int x = 0; x < boardHeight; x++)
        {
            List<int> colSolution = FindColumnSolution(x);

            for (int i = 0; i < colSolution.Count; i++)
            {
                Vector2 numPosition = new Vector2(x, boardHeight + (i * 0.8f));

                Num newNum = Instantiate(num, numPosition, transform.rotation, this.transform);
                newNum.GetComponent<TMPro.TextMeshPro>().text = colSolution[i].ToString();
            }
        }
    }


    private List<int> FindRowSolution(int rowY)
    {
        // Returns the hint numbers for a specified row
        List<Tile> rowTiles = new List<Tile>();
   
        // Creates a list of all tiles in the specified row
        for (int i = 0; i < boardWidth; i++)
        {
            rowTiles.Add(_board[i,rowY]);
        }

        // Order reversed because the UI numbers are generated right to left
        rowTiles.Reverse();

        return FindListSolution(rowTiles);
    }


    private List<int> FindColumnSolution(int colX)
    {
        // Returns the hint numbers for a specified column
        List<Tile> colTiles = new List<Tile>();

        // Creates a list of all tiles in the specified column
        for (int i = 0; i < boardWidth; i++)
        {
            colTiles.Add(_board[colX, i]);
        }

        return FindListSolution(colTiles);
    }


    private List<int> FindListSolution(List<Tile> tiles)
    {
        // Returns a List of numbers to be used as a game hint
        List<int> solution = new List<int>();
        int concurrentTiles = 0;

        foreach (Tile tile in tiles)
        {
            // If the current tiles solved state is 1, add 1 to the block length
            if (tile.solved == 1)
            {
                concurrentTiles++;
            }
            else
            {
                // If the current tiles solved state isnt 1 and this tile is at the end of a block,
                // add the block length to the solution and reset the block length
                if (concurrentTiles != 0)
                {
                    solution.Add(concurrentTiles);
                }
                concurrentTiles = 0;
            }
        }

        // Extra block check incase there is a block at the end of a row/collumn,
        // this wouldnt be added normally as there is no X tile after it
        if (concurrentTiles != 0)
        {
            solution.Add(concurrentTiles);
        }

        // If there arent any tiles in the row/collumn, add 0 to the solution
        if (solution.Count == 0)
        {
            solution.Add(0);
        }


        return solution;
    }
}
