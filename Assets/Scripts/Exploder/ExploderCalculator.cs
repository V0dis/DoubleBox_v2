using UnityEngine;

public class ExploderCalculator : MonoBehaviour
{
    [SerializeField] private float _baseExplosionForce = 20;
    
    protected internal  ExplosionData MakeExplosionData(Cube explosionCube)
    {
        Vector3 explosionPosition = explosionCube.transform.position;
        float explosionForce = _baseExplosionForce / CalculateScale(explosionCube);

        return new ExplosionData(explosionPosition, explosionForce);
    }

    protected internal float CalculateScale(Cube cube)
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

    public readonly struct ExplosionData
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
