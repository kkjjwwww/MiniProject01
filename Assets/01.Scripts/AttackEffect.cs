using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField] private AttackEffect effectPrefab;

    private Vector3 defaultScale;

    private void Awake()
    {
        defaultScale = transform.localScale;
    }
    public void Init(AttackEffect prefab, Transform parentTransform, float scaleMultplier = 1f)
    {
        this.effectPrefab = prefab;

        if (parentTransform != null)
        {
            transform.SetParent(parentTransform, true);

            transform.localPosition = Vector3.zero;
            
        }
        transform.localScale = defaultScale * scaleMultplier;
    }

    public void DisableSelf()
    {
        transform.localScale = defaultScale;
        transform.SetParent (null);

        if (effectPrefab != null && ObjectPoolManager.instance != null)
        {
            ObjectPoolManager.instance.ReturnObject(effectPrefab, this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
