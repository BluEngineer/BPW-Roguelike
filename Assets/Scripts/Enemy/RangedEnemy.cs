using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject bulletType;
    public int bulletSpeed;
    public int coinDropAmount;

    public float minShootTime;
    public float maxShootTime;

    public GameObject coin;
    public GameObject enemyObject;
    public GameObject deathEffect;

    public Vector3 bulletSize = new Vector3(1, 1, 1);

    public GameObject tempAudioPlayer;
    public AudioSource audioSource;
    public AudioClip hitSound;

    public void Start()
    {
        Health = enemyMaxHP;
        healthBar.maxValue = Health;
        healthBar.value = Health;
        uiName.text = name;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        healthBar.value = Health;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() && collision.gameObject.CompareTag("PlayerBullet"))
        {
            healthBar.value = Health;
            GameObject.Destroy(collision.gameObject);
        }


    }

    public void AttackBehaviour()
    {
        if (!GameManager.Instance.playerDead)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            Debug.DrawLine(transform.position, Player.transform.position);

            Vector3 pos = Player.transform.position;
            Vector3 dir = (gameObject.transform.position - Player.transform.position).normalized;
            Debug.DrawLine(pos, pos + dir * 10, Color.red, Mathf.Infinity);

            GameObject bullet = Instantiate(bulletType);
            bullet.transform.position = gameObject.transform.position;

            bullet.GetComponent<Rigidbody>().velocity = ((new Vector3(dir.x, dir.y, dir.z)) * -1) * bulletSpeed;
            bullet.GetComponent<Bullet>().bulletDamage = enemyDamage;
            bullet.transform.localScale = bulletSize;
        }
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
        audioSource.PlayOneShot(hitSound);

        Debug.Log("Enemy Damaged");
    }

    protected override void Die()
    {

        for (int i = 0; i < coinDropAmount; i++)
        {
            GameObject dropCoin = Instantiate(coin, transform.position, Quaternion.identity);
            dropCoin.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)), ForceMode.Impulse);
        }

        Instantiate(tempAudioPlayer, transform.position, Quaternion.identity);
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(enemyObject);
    }


}


