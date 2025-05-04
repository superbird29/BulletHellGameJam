using System;
using System.Collections;
using System.Collections.Generic;
using ND_VariaBULLET;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
//[CreateAssetMenu(menuName ="Assets/Enemy/EnemyGroupWave")]
public class EnemyGroupWave : ScriptableObject
{
    float waitTime;
    EnemyGroupManager group;
}
