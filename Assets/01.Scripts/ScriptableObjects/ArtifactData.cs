using UnityEngine;

public enum ArtifactType
{
    StatModifier,
    TriggeredEffect,
}

[CreateAssetMenu(fileName = "NewArtifactData", menuName = "Item/ArtifactData")]     
public class ArtifactData : ItemData
{
    public ArtifactType artfifactType;

    public float effectCoolDown;
}
