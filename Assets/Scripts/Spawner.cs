using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _minCount = 2;
    [SerializeField] private int _maxCount = 6;

    private List<Cube> _newCubes = new List<Cube>();

    public List<Cube> SpawnCubes(Cube clickedCube, float chanceMultiplier, float sizeMultiplier)
    {
        _newCubes.Clear();

        int count = Random.Range(_minCount, _maxCount + 1);

        for (int i = 0; i < count; i++)
        {
            float spawnRadius = clickedCube.transform.localScale.x / 2;

            Vector3 spawnPoint = clickedCube.transform.position + Random.insideUnitSphere * spawnRadius;

            SpawnSingleCube(spawnPoint, clickedCube, chanceMultiplier, sizeMultiplier);
        }

        return new List<Cube>(_newCubes);
    }

    public void DeleteCube(Cube clickedCube)
    {
        Debug.Log("пркол");
        
        if(clickedCube != null) 
            Destroy(clickedCube.gameObject);
    }
    
    private void SpawnSingleCube(Vector3 spawnPosition, Cube clickedCube, float chanceMultiplier, float sizeMultiplier)
    {
        Cube newCube = Instantiate(clickedCube, spawnPosition, Quaternion.identity);
        
        newCube.transform.localScale *= sizeMultiplier;

        float parentSplitChance = clickedCube.SplitChance;
        float newSplitChance = parentSplitChance * chanceMultiplier;

        newCube.Initialize(newSplitChance, Random.ColorHSV());
        
        _newCubes.Add(newCube);
    }
}