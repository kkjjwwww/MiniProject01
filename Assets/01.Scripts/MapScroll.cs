using UnityEngine;

public class MapScroll : MonoBehaviour
{
    [SerializeField] private float mapWidth = 18f;
    [SerializeField] private float mapHeight = 10f;

    private Transform playerTransform;

    private void Start()
    {
        if (PlayerController.instance != null)
        {
            playerTransform = PlayerController.instance.transform;
        }
    }

    private void Update()
    {
        if (playerTransform == null) return;

        RepositionMap();
    }

    private void RepositionMap()
    {
        Vector3 playerPos = playerTransform.position;
        Vector3 mapPos = transform.position;

        float diffX = playerPos.x - mapPos.x;
        float diffY = playerPos.y - mapPos.y;

        if (Mathf.Abs(diffX) > mapWidth * 1.5f)
        {
            float moveX = Mathf.Sign(diffX) * mapWidth * 3f;
            transform.position += new Vector3(moveX, 0f, 0f);
        }

        if (Mathf.Abs(diffY) > mapHeight * 1.5f)
        {
            float moveY = Mathf.Sign(diffY) * mapHeight * 3f;
            transform.position += new Vector3( 0f,moveY, 0f);
        }
    }
}
