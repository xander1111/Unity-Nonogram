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
    public int state = 0;
    /*
     *  state ids:
     *   0: empty
     *  -1: x
     *  1: filled
     */

    private SpriteRenderer _sr;
    private GameManager _gm;


    private void Awake()
    {
        _sr = this.GetComponent<SpriteRenderer>();
        _gm = FindObjectOfType<GameManager>();
    }


    private void OnMouseOver()
    {
        //Checks for left click
        if (Input.GetMouseButtonDown(0))
        {
            //Resets the tile if state isnt empty already
            if (state != 0)
            {
                state = 0;
                _sr.sprite = empty;
            }
            else
            //Sets the tile state to filled
            {
                state = 1;
                _sr.sprite = filled;
            }

            //Checks whether the puzzle has been solved
            _gm.CheckIfSolved();
        } 
        //Repeats process for right click
        else if (Input.GetMouseButtonDown(1))
        {
            if (state != 0)
            {
                state = 0;
                _sr.sprite = empty;
            }
            else
            {
                state = -1;
                _sr.sprite = x;
            }

            _gm.CheckIfSolved();
        }
    }
}
