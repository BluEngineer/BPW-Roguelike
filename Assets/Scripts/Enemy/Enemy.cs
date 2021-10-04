using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public int health = 100;
    public string name;
    public Text uiName;
    public Sprite enemySprite;
    public int damage;
    public Slider healthBar;
    public float minShootTime;
    public float maxShootTime;

    public abstract void TakeDamage(int damage);

}
