using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHerb", menuName = "HarvestItem/Herb")]
public class Herb : ScriptableObject
{
    public string herbName;
    public string herbRegion;
    public string herbType;
    public string herbDescription;
}
