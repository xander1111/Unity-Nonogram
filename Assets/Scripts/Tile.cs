using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Sprite empty;
    public Sprite filled;
    public Sprite x;

    public int solved;

    /*
     *  _state ids:
     *   0: empty
     *  -1: x
     *  1: filled
     */
    [SerializeField]
    private int _state = 0;
    private SpriteRenderer _sr;


    private void Start()
    {
        _sr = this.GetComponent<SpriteRenderer>();
    }



    private void OnMouseOver()
    {
        //
        if (Input.GetMouseButtonDown(0))
        {
            if (_state != 0)
            {
                _state = 0;
                _sr.sprite = empty;
            }
            else
            {
                _state = 1;
                _sr.sprite = filled;

            }
        } 
        else if (Input.GetMouseButtonDown(1))
        {
            if (_state != 0)
            {
                _state = 0;
                _sr.sprite = empty;
            }
            else
            {
                _state = -1;
                _sr.sprite = x;
            }
        }
    }

    public void GeneratePuzzle()
    {
        //Randomizes solved state for this tile only
        solved = Random.Range(-1, 1);

    }
}
