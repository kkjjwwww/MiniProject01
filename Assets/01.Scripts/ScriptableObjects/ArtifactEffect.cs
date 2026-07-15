using UnityEngine;

public abstract class ArtifactEffect : ScriptableObject
{
    public abstract void OnApply(PlayerController player, int level);

    public abstract void OnRemove(PlayerController player, int level);

    public virtual void OnUpdate(PlayerController player, int level)
    {

    }
}
