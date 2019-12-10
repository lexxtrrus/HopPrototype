using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPosition : MonoBehaviour
{
    private float strafe;
    public float Strafe => strafe;
    private float screenCenter;
    private void Start()
    {
        screenCenter = Screen.width * 0.5f;
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        var mousePositions = Input.mousePosition.x;
        if (mousePositions > screenCenter)
        {
            strafe = 2 * (mousePositions - screenCenter) / screenCenter;
        }
        else
        {
            strafe = 1f - mousePositions / screenCenter;
            strafe *= -2f;
        }
    }
}
