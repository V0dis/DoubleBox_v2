using UnityEngine;

public class ExploderApplier : MonoBehaviour
{
    public void Explode(Collider[] colliders, ExploderCalculator.ExplosionData explosionData)
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
}
