using System.Runtime.CompilerServices;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    [SerializeField] private Transform rotationObject;

    private Camera camera;
    
    void Start()
    {
        camera = Camera.main;
        if (rotationObject == null )
        {
            rotationObject = transform;
        }
    }
    private void Update()
    {
        RotateToMouse();
    }


    private void RotateToMouse()
    {
        if (camera == null) return;
        Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        Vector2 dir = (mouseWorldPosition - rotationObject.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        rotationObject.rotation = Quaternion.Euler(0,0,angle);
    }   
}
