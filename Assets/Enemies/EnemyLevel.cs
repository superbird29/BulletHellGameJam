using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName ="Assets/Enemy/EnemyLevel")]
public class EnemyLevel : ScriptableObject
{
    public List<EnemyRound> rounds;
}