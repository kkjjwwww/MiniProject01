using UnityEngine;

public enum ArtifactType
{

}

[CreateAssetMenu(fileName = "NewArtifactData", menuName = "Item/ArtifactData")]     
public class ArtifactData : ItemData
{
    public ArtifactType artfifactType;
}
