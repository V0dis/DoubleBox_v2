using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    
    [SerializeField] private float _explosionRadiusMultiplier = 3;
    [SerializeField] private ExploderApplier _exploderApplier;
    [SerializeField] private ExploderCalculator _exploderCalculator;
    
    public void ExplodeNewCubes(Cube explosionCube, List<Cube> newCubes)
    {
        Collider[] colliders = newCubes
            .Select(cube => cube?.GetComponent<Collider>())
            .Where(collider => collider != null)
            .ToArray();
        
        ExploderCalculator.ExplosionData explosionData = _exploderCalculator.MakeExplosionData(explosionCube);
        
        _exploderApplier.Explode(colliders, explosionData);
    }

    public void ExplodeNearbyCubes(Cube explosionCube)
    {
        ExploderCalculator.ExplosionData explosionData = _exploderCalculator.MakeExplosionData(explosionCube);

        float explosionRadius = ExploderCalculator.GetScale(explosionCube) * _explosionRadiusMultiplier;

        Collider[] colliders = Physics.OverlapSphere(explosionData.ExplosionPosition, explosionRadius)
            .Where(cube => cube.gameObject != explosionCube.gameObject)
            .ToArray();
        
        _exploderApplier.Explode(colliders, explosionData);
    }
}