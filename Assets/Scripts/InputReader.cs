using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const int IndexLeftButtonMouse = 0;
    
    public event Action<Vector3> InputEvent;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(IndexLeftButtonMouse))
        {
            InputEvent?.Invoke(Input.mousePosition);
        }
    }
}