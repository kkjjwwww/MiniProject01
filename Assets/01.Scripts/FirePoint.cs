using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirePoint : MonoBehaviour
{
    [SerializeField] private Transform rotationObject;

    private Camera mainCamera;
    
    void Start()
    {
        mainCamera = Camera.main;
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
        if (mainCamera == null) return;
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f;

        Vector2 dir = (mouseWorldPosition - rotationObject.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        rotationObject.rotation = Quaternion.Euler(0,0,angle);
    }   
}
