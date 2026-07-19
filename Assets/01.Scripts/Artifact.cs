using UnityEngine;

public class Artifact : MonoBehaviour
{
    public ArtifactData artifactData;
    protected PlayerController player;

    public int currentLevel = 1;

    public Artifact(ArtifactData artifactData)
    {
        this.artifactData = artifactData;
    }

    public virtual void Init(PlayerController playerContoller,ArtifactData data)
    {
        player = playerContoller;
        artifactData = data;

        ApplyAllEffects();
    }
    
    public void ApplyAllEffects()
    {
        if (player == null || artifactData == null) return;
         foreach (var effect in artifactData.effects)
        {
            effect.OnApply(player, currentLevel);
        }
    }

    public void LevelUp()
    {
        RemoveAllEffects();

        currentLevel++;

        ApplyAllEffects();
    }

    private void Update()
    {
        if (player == null || artifactData == null) return;

        foreach(var effect in artifactData.effects)
        {
            effect.OnUpdate(player, currentLevel);
        }
    }
    private void RemoveAllEffects()
    {
        if (player == null || artifactData == null) return;
        foreach (var effect in artifactData.effects)
        {
            effect.OnRemove(player,currentLevel);
        }
    }
   
}
