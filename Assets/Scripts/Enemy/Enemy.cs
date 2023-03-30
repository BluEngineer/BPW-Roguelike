using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    //public int health = 100;
    public string name;
    public Text uiName;
    public Sprite enemySprite;
    public int enemyDamage;
    public Slider healthBar;
    public Text damageCounterText;
    public int enemyMaxHP;

    public int Health
    {
        get;
        set;
    }

    public int LastDamageTaken
    {
        get;
        set;
    }

    public abstract void TakeDamage(int damage);
    public virtual void ShowDamageCounters()
    {
        damageCounterText.text = LastDamageTaken.ToString();
    }

    public int GetHealth()
    {
        return Health;
    }

    protected virtual void Die()
    {

    }
}
