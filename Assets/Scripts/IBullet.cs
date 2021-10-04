using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletTypes
{
    Normal,
    DamagePlus,
    Splitter,
    Cluster,
    Homing
}

public interface IBullet
{
    int bulletDamage {get; set;}
    void BulletShoot(Gun gunParent, Quaternion _bulletRotation, float _shootPower);
    int CalculateDamage();
}
