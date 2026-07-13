using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform target;

    Vector3 currentVelocity = Vector3.zero;
    
    void LateUpdate()
    {
        if (target == null) return;
        
        Vector3 targetPosition = target.position ;
        targetPosition.z = -10;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, 0.3f);
    }
}
