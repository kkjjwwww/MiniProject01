using UnityEngine;

public abstract class ArtifactEffect : ScriptableObject
{
    public virtual void OnApply(PlayerController player, int level)
    {

    }

    public virtual void OnRemove(PlayerController player, int level)
    {

    }

    public virtual void OnUpdate(PlayerController player, int level)
    {

    }
    public virtual int GetMaxLevel()
    {
        return 1;
    }
    public virtual string GetDescriptionText(int level)
    {
        return string.Empty;
    }
}
