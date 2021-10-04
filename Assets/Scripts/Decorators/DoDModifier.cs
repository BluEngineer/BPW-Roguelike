using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DoDModifier", order = 1)]
public class DoDModifier : BulletModifier
{
    public float extraDamage;
    public float doDDamage;
    public override void OnBulletShot(Gun gunParent)
    {

    }

    public override int CalculateDamage()
    {
        return (int)extraDamage;
    }


    public override void BulletUpdate()
    {
        base.BulletUpdate();
        extraDamage += doDDamage * Time.deltaTime;

    }


}
