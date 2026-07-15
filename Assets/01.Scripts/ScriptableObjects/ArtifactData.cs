using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


public enum ModifyStatType
{
    MaxHp,
    MoveSpeed,
    Damage,
    CoolDownReduction,
}
[CreateAssetMenu(fileName = "NewArtifactData", menuName = "Item/ArtifactData")]     
public class ArtifactData : ItemData
{
    public List<ArtifactEffect> effects = new List<ArtifactEffect>();

   
}
