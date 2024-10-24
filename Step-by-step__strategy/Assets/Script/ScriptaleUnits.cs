using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit ", menuName = "Scriptale Unit")]
public class ScriptaleUnits : ScriptableObject
{
    public Faction Faction;
    public BaseUnit UnitPrefab;
}

public enum Faction
{ 
    PlayerUnit = 0,
    EnemyUnit = 1
}