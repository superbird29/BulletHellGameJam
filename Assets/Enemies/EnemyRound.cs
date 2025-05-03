using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName ="Assets/Enemy/EnemyRound")]
public class EnemyRound : ScriptableObject
{
    public List<EnemyGroupManager> waves;
}