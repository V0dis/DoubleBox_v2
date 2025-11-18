using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _minCount = 2;
    [SerializeField] private int _maxCount = 6;
    [SerializeField] private CubeSplitHandler _cubeSplitHandler;

    private List<Cube> _newCubes = new List<Cube>();
    
    public void SpawnCubes(Cube clickedCube)
    {
        _newCubes.Clear();
        
        int count = Random.Range(_minCount, _maxCount + 1);

        for (int i = 0; i < count; i++)
        {
            float spawnRadius = clickedCube.transform.localScale.x / 2;
            
            Vector3 spawnPoint = clickedCube.transform.position + Random.insideUnitSphere * spawnRadius;
            
            SpawnSingleCube(spawnPoint, clickedCube);
        }
    }

    public void DeleteCube(Cube clickedCube)
    {
        if(clickedCube != null) 
            Destroy(clickedCube.gameObject);
    }
    
    private void SpawnSingleCube(Vector3 spawnPosition, Cube clickedCube)
    {
        Cube newCube = Instantiate(clickedCube, spawnPosition, Quaternion.identity);
        
        newCube.transform.localScale *= _cubeSplitHandler.SizeMultiplier;

        float parentSplitChance = clickedCube.SplitChance;
        float newSplitChance = parentSplitChance * _cubeSplitHandler.ChanceMultiplier;

        newCube.Initialize(newSplitChance, Random.ColorHSV());
        
        _newCubes.Add(newCube);
    }

    public List<Cube> GetNewCubes => new List<Cube>(_newCubes);
}
