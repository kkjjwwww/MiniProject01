using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    public void SetSlot(Sprite sprite)
    {
        if(sprite != null && iconImage != null)
        {
            iconImage.sprite = sprite;
            iconImage.gameObject.SetActive(true);
        }
    }
}
