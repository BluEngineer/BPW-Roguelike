using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int Health { get; set; }
    int LastDamageTaken { get; set; }
    
    int GetHealth();
    void TakeDamage(int damage );
}
