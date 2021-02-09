using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Sprite empty;
    public Sprite filled;
    public Sprite x;

    // Indicates the solved state for this tile
    public int solved;
    // Indicates the tiles current state
    protected int _state = 0;
    /*
     *  state ids:
     *   0: empty
     *  -1: x
     *  1: filled
     */
    public int State {
        get { return _state; }
        set 
        { 
            _state = value;
            switch (value)
            {
                case 0:
                    _sr.sprite = empty;
                    break;
                case 1:
                    _sr.sprite = filled;
                    break;
                case -1:
                    _sr.sprite = x;
                    break;
                default:
                    break;
            }
        }
    }
    

    private SpriteRenderer _sr;
    private GameManager _gm;


    private void Awake()
    {
        _sr = this.GetComponent<SpriteRenderer>();
        _gm = FindObjectOfType<GameManager>();
    }


    private void OnMouseOver()
    {
        // In help mode, when the tile is solved, don't let the player change it
        if (State == 0 || !_gm.helpMode)
        {
            // Checks for left click
            if (Input.GetMouseButtonDown(0))
            {
                // Resets the tile if state isnt empty already
                if (State != 0)
                {
                    State = 0;
                }
                else
                // Sets the tile state to filled
                {
                    State = 1;
                }

                // Checks whether the tile has been solved
                CheckIfTileSolved(this);
            }
            // Repeats process for right click
            else if (Input.GetMouseButtonDown(1))
            {
                if (State != 0)
                {
                    State = 0;
                }
                else
                {
                    State = -1;
                }

                CheckIfTileSolved(this);
            }
        }
    }


    private bool CheckIfTileSolved(Tile tile)
    {
        if (tile.solved == tile.State)
        {
            // If this tile got solved, check if the whole puzzle is solved as a result
            _gm.CheckIfSolved();
            return true;
        }
        else
        {
            if (_gm.helpMode)
            {
                _gm.AddMistake();
                State = 0;
            }
            
            return false;
        }
    }
}
