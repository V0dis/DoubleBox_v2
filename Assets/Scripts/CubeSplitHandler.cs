using System.Collections.Generic;
using UnityEngine;

public class CubeSplitHandler : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float _chanceMultiplier = 0.5f;
    [SerializeField] private float _sizeMultiplier = 0.5f;
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;
    
    private void OnEnable()
    {
        _raycaster.RaycastCube += HundleSplitChance;
    }

    private void OnDisable()
    {
        _raycaster.RaycastCube -= HundleSplitChance;
    }

    private void HundleSplitChance(Cube clickedCube)
    {
        if (clickedCube.SplitChance >= Random.value)
        {
            List<Cube> cubes = _spawner.SpawnCubes(clickedCube, _chanceMultiplier, _sizeMultiplier);
            _exploder.ExplodeNewCubes(clickedCube, cubes);
        }
        else
        {
            _exploder.ExplodeNearbyCubes(clickedCube);
        }
        
        _spawner.DeleteCube(clickedCube);
    }
}