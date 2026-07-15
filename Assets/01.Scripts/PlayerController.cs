using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Transform rotationPivot;
    private Rigidbody2D rb;
    private Vector3 dir;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
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

        
        Attack();
        

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = dir * moveSpeed;
    }
    private void Attack()
    {
        if (currentWeapon != null && rotationPivot != null)
        {
            Vector2 attackDir = rotationPivot.right;

            currentWeapon.Attack(attackDir);
        }
    }
}
