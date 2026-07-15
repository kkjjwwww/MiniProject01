using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "NewArtifactData", menuName = "Item/ArtifactData")]     
public class ArtifactData : ItemData
{
    public List<ArtifactEffect> effects = new List<ArtifactEffect>();

   
}
