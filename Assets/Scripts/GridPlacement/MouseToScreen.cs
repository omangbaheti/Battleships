using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToScreen : MonoBehaviour
{
    private static Camera mainCam = Camera.main;
    
    public static Vector3 GetWorldPosition()
    {
        Plane plane= new Plane(Vector3.up, 0);
        Ray screenToWorldRay = mainCam.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(screenToWorldRay, out float distance))
        {
            Vector3 worldPosition = screenToWorldRay.GetPoint(distance);
            return worldPosition;
        }
        throw new Exception("Raycast not working");
    }
}
