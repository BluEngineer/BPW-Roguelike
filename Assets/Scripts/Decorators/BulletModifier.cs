using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletModifier : ScriptableObject
{

    public Bullet tempBullet;
    protected Gun gunParent;

    public int bulletDamage
    {
        get;
        set;
    }


    public virtual void BulletUpdate()
    {
        
    }
     
    public virtual void OnBulletShot(Gun _gunParent)
    {
       // Debug.Log("bullet shot called in Abstract Base Modifier");
        gunParent = _gunParent;
    }

    public virtual void OnBulletHit(Collision col)
    {
        //Debug.Log("BULLET HIT SOMETHING");
    }

    public virtual int CalculateDamage()
    {
        return 0;
    }



}
