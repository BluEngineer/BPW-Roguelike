using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ExtradamageModifier", order = 1)]
public class ExtraDamageModifier : BulletModifier
{
    public int extraDamage = 3;

    public override int CalculateDamage()
    {
        return extraDamage;
    }

}
