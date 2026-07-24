using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


public enum ModifyStatType
{
    MaxHp,
    MoveSpeed,
    Damage,
    CoolDownReduction,
    //AttackSpeed,
    MaxHp_Int,
    MoveSpeed_Int,
    Damage_Int,

}
[CreateAssetMenu(fileName = "NewArtifactData", menuName = "Item/ArtifactData")]     
public class ArtifactData : ItemData
{
    public List<ArtifactEffect> effects = new List<ArtifactEffect>();

   
}
