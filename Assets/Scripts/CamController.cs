using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    private GameManager _gm;
    private Camera _cam;
    private float _maxSize;
    private Vector3 _clickPos;
    private Vector3 _cameraClickPos;

    private Camera _stableCam;

    void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        _cam = this.GetComponent<Camera>();
        _maxSize = _gm.defaultCamSize * 1.4f;
        _stableCam = GameObject.FindGameObjectWithTag("StableCamera").GetComponent<Camera>();
    }


    void Update()
    {
        ZoomCamera();
        ScrollCamera();
    }


    private void ZoomCamera()
    {
        float newSize =_cam.orthographicSize - Input.mouseScrollDelta.y;
        _cam.orthographicSize = Mathf.Clamp(newSize, 3.0f, _maxSize);
    }


    private void ScrollCamera()
    {
        // Allows the use of the middle mouse button for moving the camera in a similar way to how a map would

        // Set the size of the unmoving camera to the same as the main camera,
        // this makes the amount the camera moves change when zoomed in more or less
        _stableCam.orthographicSize = _cam.orthographicSize;

        if (Input.GetMouseButtonDown(2))
        {
            // Set the original position and camera position when the middle mouse button was clicked
            _clickPos = _stableCam.ScreenToWorldPoint(Input.mousePosition);
            _cameraClickPos = transform.position;
        }

        if (Input.GetMouseButton(2))
        {
            // Find how much the mouse has moved from the click position
            Vector3 newPosDelta = _stableCam.ScreenToWorldPoint(Input.mousePosition) - _clickPos;

            // newPos is the position change applied to the camera click position
            Vector3 newPos = _cameraClickPos - newPosDelta;

            transform.position = newPos;
        }

        // offset range because the center of the board is at (boardWidth/, boardHeight/2), not (0, 0)
        float clampX = Mathf.Clamp(transform.position.x, _gm.boardWidth * -1, _gm.boardWidth * 2);
        float clampY = Mathf.Clamp(transform.position.y, _gm.boardHeight * -1, _gm.boardHeight * 2);
        Vector3 finalPos = new Vector3(clampX, clampY, -10);

        transform.position = finalPos;
    }
}
