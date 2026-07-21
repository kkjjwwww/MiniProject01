using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    //[SerializeField] float moveSpeed = 5f;

    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Transform rotationPivot;
    private Rigidbody2D rb;
    private Vector3 dir;

    public int currentLevel = 1;
    public float currentExp = 0f;
    public float maxExp = 100;
    [SerializeField] private float increaseMaxExpPerLevel = 1.2f;

    [SerializeField] private float invincibleTime = 0.5f;
    private bool invincible = false;
    private SpriteRenderer sr;
    Color originColor;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
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
#if UNITY_EDITOR
        if (Keyboard.current != null && Keyboard.current.lKey.wasPressedThisFrame)
        {
            UI_LevelUp.instance.OpenLevelUpWindow();
        }
#endif
    }


    private void FixedUpdate()
    {
        float currentMoveSpeed = PlayerStats.instance.finalMoveSpeed;
        rb.linearVelocity = dir * currentMoveSpeed;
    }
    private void Attack()
    {
        if (currentWeapon != null && rotationPivot != null)
        {
            Vector2 attackDir = rotationPivot.right;

            currentWeapon.Attack(attackDir);
        }
    }

    public void AddExp(float value)
    {
        currentExp += value;
        Debug.Log($"현재 경험치{currentExp}/{maxExp}");
        if (UIManager.instance != null)
        {
            UIManager.instance.UpdateExpUI(currentExp, maxExp, currentLevel);
        }
        while (currentExp >= maxExp)
        {
            LevelUp();
        }
    }
    private void LevelUp()
    {
        currentExp -= maxExp;
        currentLevel++;

        maxExp = Mathf.Round(maxExp * increaseMaxExpPerLevel);
        Debug.Log($"레벨업 현재레벨{currentLevel}");

        if(UIManager.instance != null)
        {
            UIManager.instance.UpdateExpUI(currentExp, maxExp, currentLevel);
        }

        LevelUpAchieve();
    }
    private void LevelUpAchieve()
    {
        if (UI_LevelUp.instance != null)
        {
            UI_LevelUp.instance.OpenLevelUpWindow();
        }
    }

    public void OnTakeDamage(float damage)
    {
        if (invincible) return;

        PlayerStats.instance.TakeDamage(damage);
        StartCoroutine(InvinciblityCoroutine());
    }

    private IEnumerator InvinciblityCoroutine()
    {
        invincible = true;
        sr.color = Color.white;

        yield return new WaitForSeconds(invincibleTime);

        sr.color = originColor;

        invincible = false;
    }
    

}

