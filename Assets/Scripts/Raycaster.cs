using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private InputReader _inputReader;
    
    public event Action<Cube> RaycastCube; 

    private void OnEnable()
    {
        _inputReader.InputEvent += HandleInput;
    }

    private void OnDisable()
    {
        _inputReader.InputEvent -= HandleInput;
    }

    private void HandleInput(Vector3 mousePosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        if(Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.TryGetComponent(out Cube cube))
            {
                RaycastCube?.Invoke(cube);
            }
        }
    }   
}