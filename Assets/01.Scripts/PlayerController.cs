using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector3 dir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current == null ) return;
        float x = 0;
        float y = 0;
        if (Keyboard.current.aKey.isPressed)
            x = -1f;
        if (Keyboard.current.wKey.isPressed)
            y = 1f;
        if (Keyboard.current.sKey.isPressed)
            y = -1f;
        if (Keyboard.current.dKey.isPressed)
            x = 1f;

        dir = new Vector3(x, y).normalized;

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = dir * moveSpeed;
    }
}
