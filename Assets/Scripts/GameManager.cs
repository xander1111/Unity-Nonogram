using System.Text.RegularExpressions;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool helpMode = true;
    public TMPro.TextMeshProUGUI mistakeCounter;
    public Board board;
    public Camera cam;

    public int boardWidth;
    public int boardHeight;

    public float defaultCamSize;

    private Camera _cam;
    private Tile[] _tiles;
    

    private void Awake()
    {
        board.CreateGame(boardWidth, boardHeight);

        _cam = Camera.main;
        SetCamera();

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


    public void SetCamera()
    {
        //Centers the camera on the board and scales it so the whole board is in frame
        float centerWidth = boardWidth%2 == 0 ? (boardWidth/2) - 0.5f: boardWidth/2;
        float centerHeight = boardHeight%2 == 0 ? (boardHeight/2) - 0.5f: boardHeight/2;

        Vector3 boardCenter = new Vector3(centerWidth, centerHeight, -10);

        _cam.transform.position = boardCenter;

        // If the board is wider than the screens aspect ratio, scale by width, otherwise scale by height

        /* Height scale function makes sure there is 1 tile of space on the top and bottom
         * of the board when using a 5 height board
         * 
         * Width scale function does the same thing but horizontal. Its more complex because
         * the Unity camera scales aspect ratio by adjusting the width of the camera, this is why
         * the width scale uses the aspect ratio while the height doesnt.
         */
        if (boardWidth/boardHeight >= _cam.aspect)
        {
            _cam.orthographicSize = (boardWidth + 2) / (2 * _cam.aspect);
        }
        else
        {
            _cam.orthographicSize = boardHeight * 0.7f;
        }

        defaultCamSize = _cam.orthographicSize;
    }
}
