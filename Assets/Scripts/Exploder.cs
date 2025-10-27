using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _baseExplosionForce = 20;
    
    public void Explode(Cube explosionCube)
    {
        float cubeSize = explosionCube.transform.localScale.x;
        float explosionForce = _baseExplosionForce / cubeSize;
        float explosionRadius = explosionCube.gameObject.transform.localScale.x * 3;
        Vector3 explosionPosition = explosionCube.gameObject.transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        
        foreach (var cube in colliders)
        {
            if (cube.gameObject == explosionCube.gameObject)
                continue;
            
            Rigidbody rb = cube.gameObject.GetComponent<Rigidbody>();

            Vector3 cubePosition = cube.transform.position;
            Vector3 explosionCubePosition = explosionCube.transform.position;
            Vector3 direction = (cubePosition - explosionCubePosition).normalized;
            
            float distance = Vector3.Distance(cubePosition, explosionCubePosition);
            
            if (distance <= 0.1f) direction = Vector3.up;

            float actualExplosionForce = explosionForce / distance;
            
            if (rb != null)
                rb.AddForce(direction * actualExplosionForce, ForceMode.Impulse);
        }
    }
}