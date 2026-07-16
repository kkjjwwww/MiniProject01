using UnityEngine;

public class ExpToken : MonoBehaviour
{
    public float expValue = 10;
    public float moveSpeed ;
    public float magnetRange;

    private Transform playerTransform;
    private bool isTargetingPlayer = false;
    private ExpToken originPrefab;

    private void OnEnable()
    {
        isTargetingPlayer = true;
        if (PlayerController.instance != null)
        {
            playerTransform = PlayerController.instance.transform;
        }
    }
    private void InitToken(ExpToken prefab, float value)
    {
        originPrefab = prefab;
        expValue = value;
    }

    private void Update()
    {
        if (playerTransform == null) { return; }
        if (!isTargetingPlayer)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance <= magnetRange)
            {
                isTargetingPlayer=true;
            }
        }
        if (isTargetingPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, playerTransform.position) < 0.2f)
            {

            }
        }
    }
    private void DrainExp()
    {
        if (PlayerController.instance != null)
        {
            Debug.Log($"°æÇèÄ¡ {expValue} È¹µæ");
        }
        if (originPrefab != null && ObjectPoolManager.instance != null)
        {
            ObjectPoolManager.instance.returnObject(originPrefab, this);
        }else
        {
            Destroy(gameObject);
        }
    }


}
