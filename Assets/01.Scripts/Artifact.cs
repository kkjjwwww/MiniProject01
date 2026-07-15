using UnityEngine;

public class Artifact : MonoBehaviour
{
    public ArtifactData artifactData;
    protected PlayerController player;

    public virtual void Init(PlayerController playerContoller)
    {
        player = playerContoller;
        OnEquip();
    }
    protected virtual void OnEquip()
    {

    }
   
}
