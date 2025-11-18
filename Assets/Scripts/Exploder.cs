using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _baseExplosionForce = 20;
    [SerializeField] private float _explosionRadiusMultiplier = 3;

    public void ExplodeNewCubes(Cube explosionCube, List<Cube> newCubes)
    {
        ExplosionData explosionData = MakeExplosionData(explosionCube);

        Collider[] colliders = newCubes
            .Select(cube => cube?.GetComponent<Collider>())
            .Where(collider => collider != null)
            .ToArray();
        
        Explode(colliders, explosionData);
    }

    public void ExplodeNearbyCubes(Cube explosionCube)
    {
        float explosionRadius = GetScale(explosionCube) * _explosionRadiusMultiplier;
        
        ExplosionData explosionData = MakeExplosionData(explosionCube);

        Collider[] colliders = Physics.OverlapSphere(explosionData.ExplosionPosition, explosionRadius)
            .Where(cube => cube.gameObject != explosionCube.gameObject)
            .ToArray();
        
        Explode(colliders, explosionData);
    }

    private void Explode(Collider[] colliders, ExplosionData explosionData)
    {
        foreach (var cube in colliders)
        {
            float minDistance = 0.01f;
            
            if (cube.gameObject.TryGetComponent(out Rigidbody rigidbody))
            {
                Vector3 offset = cube.transform.position - explosionData.ExplosionPosition;
                
                if(offset.sqrMagnitude < minDistance * minDistance)
                    offset = Random.onUnitSphere * minDistance;
                
                rigidbody.AddForce(offset.normalized * explosionData.ExplosionForce, ForceMode.Impulse);
            }
        }
    }
    
    private ExplosionData MakeExplosionData(Cube explosionCube)
    {
        Vector3 explosionPosition = explosionCube.transform.position;
        float explosionForce = _baseExplosionForce / GetScale(explosionCube);

        return new ExplosionData(explosionPosition, explosionForce);
    }

    private float GetScale(Cube cube)
    {
        Vector3 CubeScale = cube.gameObject.transform.lossyScale;

        float half = 0.5f;
        
        float mediumOfSizes = half * Mathf.Sqrt(
            CubeScale.x * CubeScale.x +
            CubeScale.y * CubeScale.y +
            CubeScale.z * CubeScale.z
        );

        return mediumOfSizes;
    }
    
    private readonly struct ExplosionData
    {
        public readonly float ExplosionForce;
        public readonly Vector3 ExplosionPosition;
        
        public ExplosionData(Vector3 explosionPosition, float explosionForce)
        {
            ExplosionPosition = explosionPosition;
            ExplosionForce = explosionForce;
        }
    }
}