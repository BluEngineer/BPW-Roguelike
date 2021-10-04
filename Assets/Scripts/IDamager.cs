using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamager
{
    void InflictDamage(IDamageable damageReceiver);
    void AddDamage(int bonusDamage);
}
