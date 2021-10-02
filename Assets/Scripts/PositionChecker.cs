using UnityEngine;

public class PositionChecker : MonoBehaviour
{
    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    public void CheckPosition(Rigidbody2D rigidbody)
    {
        //declare screen parameters
        float Width = _mainCam.orthographicSize * 2 * _mainCam.aspect;
        float Height = _mainCam.orthographicSize * 2;

        //devide screen in pieces
        float top = Height / 2;
        float bottom = top * -1;
        float right = Width / 2;
        float left = right * -1;

        //check player position and teleport him
        if (rigidbody.position.x > right)
            rigidbody.position = new Vector2(left, rigidbody.position.y);
        if (rigidbody.position.x < left)
            rigidbody.position = new Vector2(right, rigidbody.position.y);
        if (rigidbody.position.y > top)
            rigidbody.position = new Vector2(rigidbody.position.x, bottom);
        if (rigidbody.position.y < bottom)
            rigidbody.position = new Vector2(rigidbody.position.x, top);
    }
}
