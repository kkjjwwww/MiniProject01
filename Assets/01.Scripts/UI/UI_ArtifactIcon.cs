using UnityEngine;
using UnityEngine.UI;
public class UI_ArtifactIcon : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    public void SetArtifact(Artifact artifact)
    {
        if (artifact != null && artifact.artifactData != null && artifact.artifactData.itemIcon != null)
        {
            iconImage.sprite = artifact.artifactData.itemIcon;
            iconImage.gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
