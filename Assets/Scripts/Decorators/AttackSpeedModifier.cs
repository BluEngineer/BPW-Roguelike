using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AttackSpeedModifier", order = 1)]

public class AttackSpeedModifier : BulletModifier
{
    public float ExtraAttackSpeed;
    public float DecreaseAttackDelay()
    {
        return ExtraAttackSpeed;
    }
}
