using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPlayerController : MonoBehaviour
{
    Vector2 currentPosition;
    Vector2 changeInPosition;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                currentPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                changeInPosition = currentPosition - touch.position;

                if (Mathf.Abs(changeInPosition.x) > Mathf.Abs(changeInPosition.y))
                {
                    // rotate horizontally (parent object rotation exchanges y- and z-axes)
                    transform.localEulerAngles = new Vector3(
                        transform.localEulerAngles.x, 0, changeInPosition.x);
                }
                else
                {
                    // rotate vertically (parent object rotation exchanges y- and z-axes)
                    transform.localEulerAngles = new Vector3(
                        changeInPosition.y, 0, transform.localEulerAngles.y);
                }
            }

            if (touch.tapCount == 1)
            {
                Ray ray = Camera.main.ScreenPointToRay(currentPosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Debug.Log((hit.textureCoord.x, hit.textureCoord.y));
                }
            }
        }
    }
}
